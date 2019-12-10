using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// ���ݱ����
    /// </summary>
    public class DataTableComponent : YouYouBaseComponent
    {
        /// <summary>
        /// ��������
        /// </summary>
        public DataTableManager DataTableManager
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            DataTableManager = new DataTableManager();
        }

        public override void Shutdown()
        {
            DataTableManager.Clear();
        }

        /// <summary>
        /// �첽���ر��
        /// </summary>
        public void LoadDataTableAsync()
        {
            DataTableManager.LoadDataTableAsync();
        }
    }
}