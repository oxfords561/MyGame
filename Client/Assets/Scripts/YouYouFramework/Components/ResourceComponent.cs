using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// ��Դ���
    /// </summary>
    public class ResourceComponent : YouYouBaseComponent, IUpdateComponent
    {
        /// <summary>
        /// �����ļ�·��
        /// </summary>
        public string LocalFilePath;

        /// <summary>
        /// ��Դ������
        /// </summary>
        public ResourceManager ResourceManager
        {
            get;
            private set;
        }

        /// <summary>
        /// ��Դ���ع�����
        /// </summary>
        public ResourceLoaderManager ResourceLoaderManager
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);

            ResourceManager = new ResourceManager();
            ResourceLoaderManager = new ResourceLoaderManager();

#if DISABLE_ASSETBUNDLE
            LocalFilePath = Application.dataPath;
#else
            LocalFilePath = Application.persistentDataPath;
#endif
        }

        #region InitStreamingAssetsBundleInfo ��ʼ��ֻ������Դ����Ϣ
        /// <summary>
        /// ��ʼ��ֻ������Դ����Ϣ
        /// </summary>
        public void InitStreamingAssetsBundleInfo()
        {
            ResourceManager.InitStreamingAssetsBundleInfo();
        }
        #endregion

        #region InitAssetInfo ��ʼ����Դ��Ϣ
        /// <summary>
        /// ��ʼ����Դ��Ϣ
        /// </summary>
        public void InitAssetInfo()
        {
            ResourceLoaderManager.InitAssetInfo();
        }
        #endregion

        /// <summary>
        /// ��ȡ·�����������
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetLastPathName(string path)
        {
            if (path.IndexOf('/') == -1)
            {
                return path;
            }
            return path.Substring(path.LastIndexOf('/') + 1);
        }

        public override void Shutdown()
        {
            ResourceManager.Dispose();
            ResourceLoaderManager.Dispose();

            GameEntry.RemoveUpdateComponent(this);
        }

        public void OnUpdate()
        {
            ResourceLoaderManager.OnUpdate();
        }

        /// <summary>
        /// ��ȡ��������Դ��·��
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public string GetSceneAssetBundlePath(string sceneName)
        {
            return string.Format("download/scenes/{0}.assetbundle", sceneName.ToLower());
        }
    }
}