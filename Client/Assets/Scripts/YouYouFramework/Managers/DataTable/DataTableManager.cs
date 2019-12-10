using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace YouYou
{
    public class DataTableManager : ManagerBase
    {
        public DataTableManager()
        {
            InitDBModel();
        }

        /// <summary>
        /// 总共需要加载的表格数量
        /// </summary>
        public int TotalTableCount = 0;

        /// <summary>
        /// 当前加载的表格数量
        /// </summary>
        public int CurrLoadTableCount = 0;

        public LocalizationDBModel LocalizationDBModel { get; private set; }

        public Sys_CodeDBModel Sys_CodeDBModel { get; private set; }
        public Sys_EffectDBModel Sys_EffectDBModel { get; private set; }
        public Sys_PrefabDBModel Sys_PrefabDBModel { get; private set; }
        public Sys_SoundDBModel Sys_SoundDBModel { get; private set; }
        public Sys_StorySoundDBModel Sys_StorySoundDBModel { get; private set; }
        public Sys_UIFormDBModel Sys_UIFormDBModel { get; private set; }
        public Sys_SceneDBModel Sys_SceneDBModel { get; private set; }
        public Sys_SceneDetailDBModel Sys_SceneDetailDBModel { get; private set; }

        /// <summary>
        /// 章表
        /// </summary>
        public ChapterDBModel ChapterDBModel { get; private set; }

        /// <summary>
        /// 关卡表
        /// </summary>
        public GameLevelDBModel GameLevelDBModel { get; private set; }

        public TaskDBModel TaskDBModel { get; private set; }

        /// <summary>
        /// 初始化DBModel
        /// </summary>
        private void InitDBModel()
        {
            //每个表都new
            Sys_CodeDBModel = new Sys_CodeDBModel();
            Sys_EffectDBModel = new Sys_EffectDBModel();
            LocalizationDBModel = new LocalizationDBModel();
            Sys_PrefabDBModel = new Sys_PrefabDBModel();
            Sys_SoundDBModel = new Sys_SoundDBModel();
            Sys_StorySoundDBModel = new Sys_StorySoundDBModel();
            Sys_UIFormDBModel = new Sys_UIFormDBModel();
            Sys_SceneDBModel = new Sys_SceneDBModel();
            Sys_SceneDetailDBModel = new Sys_SceneDetailDBModel();

            ChapterDBModel = new ChapterDBModel();
            GameLevelDBModel = new GameLevelDBModel();
            TaskDBModel = new TaskDBModel();
        }

        /// <summary>
        /// 加载表格
        /// </summary>
        public void LoadDataTable()
        {
            //每个表都 LoadData
            Sys_CodeDBModel.LoadData();
            Sys_EffectDBModel.LoadData();
            LocalizationDBModel.LoadData();
            Sys_PrefabDBModel.LoadData();
            Sys_SoundDBModel.LoadData();
            Sys_StorySoundDBModel.LoadData();
            Sys_UIFormDBModel.LoadData();
            Sys_SceneDBModel.LoadData();
            Sys_SceneDetailDBModel.LoadData();

            ChapterDBModel.LoadData();
            GameLevelDBModel.LoadData();
            TaskDBModel.LoadData();
        }

        /// <summary>
        /// 表格资源包
        /// </summary>
        public AssetBundle m_DataTableBundle;

        /// <summary>
        /// 异步加载表格
        /// </summary>
        public void LoadDataTableAsync()
        {
#if DISABLE_ASSETBUNDLE
            LoadDataTable();
#else
            GameEntry.Resource.ResourceLoaderManager.LoadAssetBundle(ConstDefine.DataTableAssetBundlePath, onComplete: (AssetBundle bundle) =>
            {
                m_DataTableBundle = bundle;
                LoadDataTable();
            });
#endif
        }

        /// <summary>
        /// 获取表格的字节数组
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public void GetDataTableBuffer(string tableName, BaseAction<byte[]> onComplete)
        {
#if DISABLE_ASSETBUNDLE
            byte[] buffer = IOUtil.GetFileBuffer(string.Format("{0}/download/DataTable/{1}.bytes", GameEntry.Resource.LocalFilePath, tableName));
            if (onComplete != null)
            {
                onComplete(buffer);
            }
#else
            GameEntry.Resource.ResourceLoaderManager.LoadAsset(GameEntry.Resource.GetLastPathName(tableName), m_DataTableBundle, onComplete: (UnityEngine.Object obj) =>
            {
                TextAsset asset = obj as TextAsset;
                if (onComplete != null)
                {
                    onComplete(asset.bytes);
                }
            });
#endif
        }

        public void Clear()
        {
            //每个表都Clear
            Sys_CodeDBModel.Clear();
            Sys_EffectDBModel.Clear();
            LocalizationDBModel.Clear();
            Sys_PrefabDBModel.Clear();
            Sys_SoundDBModel.Clear();
            Sys_StorySoundDBModel.Clear();
            Sys_UIFormDBModel.Clear();
            Sys_SceneDBModel.Clear();
            Sys_SceneDetailDBModel.Clear();

            ChapterDBModel.Clear();
            GameLevelDBModel.Clear();
            TaskDBModel.Clear();
        }
    }
}