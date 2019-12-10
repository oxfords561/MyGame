using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// 状态机组件
    /// </summary>
    public class FsmComponent : YouYouBaseComponent
    {
        /// <summary>
        /// 状态机管理器
        /// </summary>
        private FsmManager m_FsmManager;

        /// <summary>
        /// 状态机的临时编号
        /// </summary>
        private int m_TemFsmId = 0;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_FsmManager = new FsmManager();
        }

        #region Create 创建状态机
        /// <summary>
        /// 创建状态机
        /// </summary>
        /// <typeparam name="T">拥有者类型</typeparam>
        /// <param name="owner">拥有者</param>
        /// <param name="states">状态数组</param>
        /// <returns></returns>
        public Fsm<T> Create<T>(T owner, FsmState<T>[] states) where T : class
        {
            return m_FsmManager.Create<T>(m_TemFsmId++, owner, states);
        }
        #endregion

        #region DestroyFsm 销毁状态机
        /// <summary>
        /// 销毁状态机
        /// </summary>
        /// <param name="fsmId"></param>
        public void DestroyFsm(int fsmId)
        {
            m_FsmManager.DestroyFsm(fsmId);
        }
        #endregion

        public override void Shutdown()
        {
            m_FsmManager.Dispose();
        }
    }
}