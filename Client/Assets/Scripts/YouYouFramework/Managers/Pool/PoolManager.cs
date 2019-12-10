using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YouYou
{
    /// <summary>
    /// �ع�����
    /// </summary>
    public class PoolManager : ManagerBase, IDisposable
    {
        /// <summary>
        /// ������
        /// </summary>
        public ClassObjectPool ClassObjectPool
        {
            get;
            private set;
        }

        /// <summary>
        /// ��Ϸ��������
        /// </summary>
        public GameObjectPool GameObjectPool
        {
            get;
            private set;
        }

        /// <summary>
        /// ��Դ����
        /// </summary>
        public ResourcePool AssetBundlePool
        {
            get;
            private set;
        }

        /// <summary>
        /// ������Դ��
        /// </summary>
        public Dictionary<AssetCategory, ResourcePool> AssetPool
        {
            get;
            private set;
        }

        public PoolManager()
        {
            ClassObjectPool = new ClassObjectPool();
            GameObjectPool = new GameObjectPool();

            AssetBundlePool = new ResourcePool("AssetBundlePool");

            AssetPool = new Dictionary<AssetCategory, ResourcePool>();
            //ȷ����Ϸ�տ�ʼ���е�ʱ�� ������Դ���Ѿ���ʼ������
            var enumerator = Enum.GetValues(typeof(AssetCategory)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                AssetCategory assetCategory = (AssetCategory)enumerator.Current;
                if (assetCategory == AssetCategory.None)
                {
                    continue;
                }
                AssetPool[assetCategory] = new ResourcePool(assetCategory.ToString());
            }
        }

        /// <summary>
        /// �ͷ�������
        /// </summary>
        public void ReleaseClassObjectPool()
        {
            ClassObjectPool.Release();
        }

        /// <summary>
        /// �ͷ���Դ����
        /// </summary>
        public void ReleaseAssetBundlePool()
        {
            AssetBundlePool.Release();
        }

        /// <summary>
        /// �ͷŷ�����Դ����������Դ
        /// </summary>
        public void ReleaseAssetPool()
        {
            var enumerator = Enum.GetValues(typeof(AssetCategory)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                AssetCategory assetCategory = (AssetCategory)enumerator.Current;
                if (assetCategory == AssetCategory.None)
                {
                    continue;
                }
                AssetPool[assetCategory].Release();
            }
        }

        public void Dispose()
        {
            ClassObjectPool.Dispose();
            GameObjectPool.Dispose();
        }
    }
}