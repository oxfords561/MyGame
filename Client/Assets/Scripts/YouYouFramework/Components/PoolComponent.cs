using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// 对象池组件
    /// </summary>
    public class PoolComponent : YouYouBaseComponent, IUpdateComponent
    {
        [Header("锁定的资源包")]
        /// <summary>
        /// 锁定的资源包（不会释放）
        /// </summary>
        public string[] LockedAssetBundle;

        /// <summary>
        /// 锁定的资源包数组长度
        /// </summary>
        private int m_LockedAssetBundleLength;

        public PoolManager PoolManager
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            PoolManager = new PoolManager();
            GameEntry.RegisterUpdateComponent(this);

            m_ReleaseClassObjectNextRunTime = Time.time;
            m_ReleaseAssetBundleNextRunTime = Time.time;
            m_ReleaseAssetNextRunTime = Time.time;

            InitGameObjectPool();

            m_InstanceResourceDic = new Dictionary<int, ResourceEntity>();
            FontDic = new Dictionary<string, ResourceEntity>();
        }

        protected override void OnStart()
        {
            base.OnStart();

            m_LockedAssetBundleLength = LockedAssetBundle.Length;

            InitClassReside();
        }

        /// <summary>
        /// 检查资源包是否锁定
        /// </summary>
        /// <param name="assetBundleName">资源包名称</param>
        /// <returns></returns>
        public bool CheckAssetBundleIsLock(string assetBundleName)
        {
            for (int i = 0; i < m_LockedAssetBundleLength; i++)
            {
                if (LockedAssetBundle[i].Equals(assetBundleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 初始化常用类常驻数量
        /// </summary>
        private void InitClassReside()
        {
            GameEntry.Pool.SetClassObjectResideCount<HttpRoutine>(3);
            GameEntry.Pool.SetClassObjectResideCount<Dictionary<string, object>>(3);
            GameEntry.Pool.SetClassObjectResideCount<AssetBundleLoaderRoutine>(10);
            GameEntry.Pool.SetClassObjectResideCount<AssetLoaderRoutine>(10);
            GameEntry.Pool.SetClassObjectResideCount<ResourceEntity>(10);
            GameEntry.Pool.SetClassObjectResideCount<MainAssetLoaderRoutine>(30);
        }

        #region SetResideCount 设置类常驻数量
        /// <summary>
        /// 设置类常驻数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        public void SetClassObjectResideCount<T>(byte count) where T : class
        {
            PoolManager.ClassObjectPool.SetResideCount<T>(count);
        }
        #endregion

        #region DequeueClassObject 取出一个对象
        /// <summary>
        /// 取出一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DequeueClassObject<T>() where T : class, new()
        {
            return PoolManager.ClassObjectPool.Dequeue<T>();
        }
        #endregion

        #region EnqueueClassObject 对象回池
        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="obj"></param>
        public void EnqueueClassObject(object obj)
        {
            PoolManager.ClassObjectPool.Enqueue(obj);
        }
        #endregion

        #region 变量对象池

        /// <summary>
        /// 变量对象池锁
        /// </summary>
        private object m_VarObjectLock = new object();

#if UNITY_EDITOR
        /// <summary>
        /// 在监视面板显示的信息
        /// </summary>
        public Dictionary<Type, int> VarObjectInspectorDic = new Dictionary<Type, int>();
#endif

        /// <summary>
        /// 取出一个变量对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T DequeueVarObject<T>() where T : VariableBase, new()
        {
            lock (m_VarObjectLock)
            {
                T item = DequeueClassObject<T>();
#if UNITY_EDITOR
                Type t = item.GetType();
                if (VarObjectInspectorDic.ContainsKey(t))
                {
                    VarObjectInspectorDic[t]++;
                }
                else
                {
                    VarObjectInspectorDic[t] = 1;
                }
#endif
                return item;
            }
        }

        /// <summary>
        /// 变量对象回池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void EnqueueVarObject<T>(T item) where T : VariableBase
        {
            lock (m_VarObjectLock)
            {
                EnqueueClassObject(item);
#if UNITY_EDITOR
                Type t = item.GetType();
                if (VarObjectInspectorDic.ContainsKey(t))
                {
                    VarObjectInspectorDic[t]--;
                    if (VarObjectInspectorDic[t] == 0)
                    {
                        VarObjectInspectorDic.Remove(t);
                    }
                }
#endif
            }
        }

        #endregion

        public override void Shutdown()
        {
            PoolManager.Dispose();
        }

        /// <summary>
        /// 释放类对象池间隔
        /// </summary>
        [SerializeField]
        public int ReleaseClassObjectInterval = 30;

        /// <summary>
        /// 下次释放类对象运行时间
        /// </summary>
        private float m_ReleaseClassObjectNextRunTime = 0f;


        /// <summary>
        /// 释放AssetBundle池间隔
        /// </summary>
        [SerializeField]
        public int ReleaseAssetBundleInterval = 60;

        /// <summary>
        /// 下次释放AssetBundle池运行时间
        /// </summary>
        private float m_ReleaseAssetBundleNextRunTime = 0f;

        /// <summary>
        /// 释放Asset池间隔
        /// </summary>
        [SerializeField]
        public int ReleaseAssetInterval = 120;

        /// <summary>
        /// 下次释放Asset池运行时间
        /// </summary>
        private float m_ReleaseAssetNextRunTime = 0f;

        /// <summary>
        /// 显示分类资源池
        /// </summary>
        [SerializeField]
        public bool ShowAssetPool = false;

        public void OnUpdate()
        {
            if (Time.time > m_ReleaseClassObjectNextRunTime + ReleaseClassObjectInterval)
            {
                m_ReleaseClassObjectNextRunTime = Time.time;
                PoolManager.ReleaseClassObjectPool();
                GameEntry.Log(LogCategory.Normal, "释放类对象池");
            }

            if (Time.time > m_ReleaseAssetBundleNextRunTime + ReleaseAssetBundleInterval)
            {
                m_ReleaseAssetBundleNextRunTime = Time.time;
                PoolManager.ReleaseAssetBundlePool();
                GameEntry.Log(LogCategory.Normal, "释放AssetBundle池");
            }

            if (Time.time > m_ReleaseAssetNextRunTime + ReleaseAssetInterval)
            {
                m_ReleaseAssetNextRunTime = Time.time;
                PoolManager.ReleaseAssetPool();
                Resources.UnloadUnusedAssets();
                GameEntry.Log(LogCategory.Normal, "释放Asset池");
            }
        }

        #region 游戏物体对象池

        /// <summary>
        /// 对象池分组
        /// </summary>
        [SerializeField]
        private GameObjectPoolEntity[] m_GameObjectPoolGroups;

        /// <summary>
        /// 初始化游戏物体对象池
        /// </summary>
        public void InitGameObjectPool()
        {
            StartCoroutine(PoolManager.GameObjectPool.Init(m_GameObjectPoolGroups, transform));
        }

        /// <summary>
        /// 从对象池中获取对象
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="prefab"></param>
        /// <param name="onComplete"></param>
        public void GameObjectSpawn(byte poolId, Transform prefab, System.Action<Transform> onComplete)
        {
            PoolManager.GameObjectPool.Spawn(poolId, prefab, onComplete);
        }

        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="instance"></param>
        public void GameObjectDespawn(byte poolId, Transform instance)
        {
            PoolManager.GameObjectPool.Despawn(poolId, instance);
        }
        #endregion

        /// <summary>
        /// 字体字典(为了持有字体的引用)
        /// </summary>
        public Dictionary<string, ResourceEntity> FontDic
        {
            private set;
            get;
        }

        #region 实例管理和分类资源池释放
        /// <summary>
        /// 克隆出来的实例资源字典
        /// </summary>
        private Dictionary<int, ResourceEntity> m_InstanceResourceDic;

        /// <summary>
        /// 注册到实例字典
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="resourceEntity"></param>
        public void RegisterInstanceResource(int instanceId, ResourceEntity resourceEntity)
        {
            //Debug.LogError("注册到实例字典instanceId=" + instanceId);
            m_InstanceResourceDic[instanceId] = resourceEntity;
        }

        /// <summary>
        /// 释放实例资源
        /// </summary>
        /// <param name="instanceId"></param>
        public void ReleaseInstanceResource(int instanceId)
        {
            //Debug.LogError("释放实例资源instanceId=" + instanceId);
            ResourceEntity resourceEntity = null;
            if (m_InstanceResourceDic.TryGetValue(instanceId, out resourceEntity))
            {
                UnspawnResourceEntity(resourceEntity);
                m_InstanceResourceDic.Remove(instanceId);
            }
        }

        /// <summary>
        /// 资源实体回池
        /// </summary>
        /// <param name="entity"></param>
        private void UnspawnResourceEntity(ResourceEntity entity)
        {
            var curr = entity.DependsResourceList.First;
            while (curr != null)
            {
                UnspawnResourceEntity(curr.Value);
                curr = curr.Next;
            }

            GameEntry.Pool.PoolManager.AssetPool[entity.Category].Unspawn(entity.ResourceName);
        }
        #endregion
    }
}