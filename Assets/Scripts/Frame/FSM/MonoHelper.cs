using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.FSM
{
    public class MonoHelper : MonoBehaviour
    {
        /// <summary>
        /// 静态单例脚本
        /// </summary>
        public static MonoHelper instance;

        static MonoHelper()
        {
            if (instance == null)
            {
                instance = new GameObject(
                    "MonoHelper").
                    AddComponent<MonoHelper>();
                //New
                instance.needUpdateEventStates = new List<State>();
            }
        }
        
        private float invokeInterval = -1;

        public float InvokeInterval
        {
            get => invokeInterval;
            set => invokeInterval = value;
        }

        /// <summary>
        /// 需要执行更新事件的状态
        /// </summary>
        private List<State> needUpdateEventStates;

        /// <summary>
        /// 添加需要执行更新事件的状态
        /// </summary>
        /// <param name="state"></param>
        public void AddUpdateEventState(State state)
        {
            if (!needUpdateEventStates.Contains(state))
            {
                needUpdateEventStates.Add(state);
            }
        }

        /// <summary>
        /// 移除需要执行更新事件的状态
        /// </summary>
        /// <param name="state"></param>
        public void RemoveUpdateEventState(State state)
        {
            if (needUpdateEventStates.Contains(state))
            {
                needUpdateEventStates.Remove(state);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                if (invokeInterval > 0)
                {
                    //间隔invokeInterval秒
                    yield return new WaitForSeconds(invokeInterval);
                }
                else
                {
                    //间隔一帧
                    yield return 0;
                }
                
                // foreach (var state in needUpdateEventStates)
                // {
                //     //执行更新事件
                //     state.OnStateUpdate(state);
                // }
                
                //TODO:执行方法
                for (int i = 0; i < needUpdateEventStates.Count; i++)
                {
                    if (needUpdateEventStates[i].OnStateUpdate != null)
                    {
                        //执行更新事件
                        needUpdateEventStates[i].OnStateUpdate(needUpdateEventStates[i]);
                    }
                }
            }
        }
    }
}