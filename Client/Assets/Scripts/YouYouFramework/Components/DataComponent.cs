using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// �������
    /// </summary>
    public class DataComponent : YouYouBaseComponent
    {
        /// <summary>
        /// ��ʱ��������
        /// </summary>
        public CacheDataManager CacheDataManager
        {
            get;
            private set;
        }

        /// <summary>
        /// ϵͳ�������
        /// </summary>
        public SysDataManager SysDataManager
        {
            get;
            private set;
        }

        /// <summary>
        /// �û��������
        /// </summary>
        public UserDataManager UserDataManager
        {
            get;
            private set;
        }

        /// <summary>
        /// �ؿ���ͼ����
        /// </summary>
        public PVEMapDataManager PVEMapDataManager
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            CacheDataManager = new CacheDataManager();
            SysDataManager = new SysDataManager();
            UserDataManager = new UserDataManager();
            PVEMapDataManager = new PVEMapDataManager();
        }

        public override void Shutdown()
        {
            CacheDataManager.Dispose();
            SysDataManager.Dispose();
            UserDataManager.Dispose();
            PVEMapDataManager.Dispose();
        }
    }
}