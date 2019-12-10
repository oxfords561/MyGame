using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// �������
    /// </summary>
    public class SceneComponent : YouYouBaseComponent, IUpdateComponent
    {
        /// <summary>
        /// ����������
        /// </summary>
        private YouYouSceneManager m_YouYouSceneManager;

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_YouYouSceneManager = new YouYouSceneManager();
        }

        /// <summary>
        /// ���س���
        /// </summary>
        /// <param name="sceneId">�������</param>
        /// <param name="showLoadingForm">�Ƿ���ʾLoading</param>
        /// <param name="onComplete">�������</param>
        public void LoadScene(int sceneId, bool showLoadingForm = false, BaseAction onComplete = null)
        {
            m_YouYouSceneManager.LoadScene(sceneId, showLoadingForm, onComplete);
        }

        public override void Shutdown()
        {
            GameEntry.RemoveUpdateComponent(this);
        }

        public void OnUpdate()
        {
            m_YouYouSceneManager.OnUpdate();
        }
    }
}