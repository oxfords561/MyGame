using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    /// <summary>
    /// ��ʱ��
    /// </summary>
    public class TimeAction
    {
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsRuning
        {
            get;
            private set;
        }

        /// <summary>
        /// ��ǰ���е�ʱ��
        /// </summary>
        private float m_CurrRunTime;

        /// <summary>
        /// ��ǰѭ������
        /// </summary>
        private int m_CurrLoop;

        /// <summary>
        /// �ӳ�ʱ��
        /// </summary>
        private float m_DelayTime;

        /// <summary>
        /// ������룩
        /// </summary>
        private float m_Interval;

        /// <summary>
        /// ѭ������(-1��ʾ ����ѭ�� 0Ҳ��ѭ��һ��)
        /// </summary>
        private int m_Loop;

        /// <summary>
        /// ��ʼ����
        /// </summary>
        private Action m_OnStar;

        /// <summary>
        /// ������ �ص�������ʾʣ�����
        /// </summary>
        private Action<int> m_OnUpdate;

        /// <summary>
        /// �������
        /// </summary>
        private Action m_OnComplete;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="delayTime">�ӳ�ʱ��</param>
        /// <param name="interval">���</param>
        /// <param name="loop">ѭ������</param>
        /// <param name="onStar"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public TimeAction Init(float delayTime, float interval, int loop, Action onStar, Action<int> onUpdate, Action onComplete)
        {
            m_DelayTime = delayTime;
            m_Interval = interval;
            m_Loop = loop;
            m_OnStar = onStar;
            m_OnUpdate = onUpdate;
            m_OnComplete = onComplete;

            return this;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Run()
        {
            //1.��Ҫ�Ȱ��Լ�����ʱ���������������
            GameEntry.Time.RegisterTimeAction(this);

            //2.���õ�ǰ���е�ʱ��
            m_CurrRunTime = Time.time;
        }

        public void Pause()
        {
            IsRuning = false;
        }

        public void Stop()
        {
            if (m_OnComplete != null)
            {
                m_OnComplete();
            }

            IsRuning = false;

            //���Լ��Ӷ�ʱ�������Ƴ�
            GameEntry.Time.RemoveTimeAction(this);
        }

        /// <summary>
        /// ÿִ֡��
        /// </summary>
        public void OnUpdate()
        {
            if (!IsRuning && Time.time > m_CurrRunTime + m_DelayTime)
            {
                //������ִ�е������ʱ�� ��ʾ�Ѿ���һ�ι����ӳ�ʱ��
                IsRuning = true;
                m_CurrRunTime = Time.time;

                if (m_OnStar != null)
                {
                    m_OnStar();
                }
            }

            if (!IsRuning) return;

            if (Time.time > m_CurrRunTime)
            {
                m_CurrRunTime = Time.time + m_Interval;

                //���´��� ���m_Interval ʱ�� ִ��һ��
                if (m_OnUpdate != null)
                {
                    m_OnUpdate(m_Loop - m_CurrLoop);
                }

                if (m_Loop > -1)
                {
                    m_CurrLoop++;
                    if (m_CurrLoop >= m_Loop)
                    {
                        Stop();
                    }
                }
            }
        }
    }
}