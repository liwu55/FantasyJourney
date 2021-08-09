using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.FSM
{
    public class State
    {
        /// <summary>
        /// 状态名称
        /// </summary>
        private string stateName;

        /// <summary>
        /// 状态名称「属性」【只读】
        /// </summary>
        public string StateName => stateName;

        /// <summary>
        /// 可以过渡到的状态
        /// </summary>
        private Dictionary<string, Func<bool>> transitions;

        private bool isRun;

        public bool IsRun => isRun;

        public State(string stateName)
        {
            this.stateName = stateName;
            transitions = new Dictionary<string, Func<bool>>();
        }

        #region 添加或删除过渡

        /// <summary>
        /// 添加过渡
        /// </summary>
        public void AddTransition(string targetStateName,
            Func<bool> conditions)
        {
            if (isRun)
            {
                Debug.LogError("当前状态已启动，无法添加新的过渡！");
                return;
            }

            if (!transitions.ContainsKey(targetStateName))
            {
                transitions.Add(targetStateName,conditions);
            }
            else
            {
                //判断新添加的方法是否已经存在于委托
                if (!Tools.Tools.DelegateContainsMethod(transitions[targetStateName], conditions))
                {
                    //添加新方法到委托
                    transitions[targetStateName] += conditions;
                }
            }
        }

        /// <summary>
        /// 替换状态过渡
        /// </summary>
        /// <param name="targetStateName"></param>
        /// <param name="conditions"></param>
        public void ReplaceTransition(string targetStateName,
            Func<bool> conditions)
        {
            if (isRun)
            {
                Debug.LogError("当前状态已启动，无法替换过渡！");
                return;
            }
            
            if (!transitions.ContainsKey(targetStateName))
            {
                //添加
                transitions.Add(targetStateName, conditions);
            }
            else
            {
                //替换
                transitions[targetStateName] = conditions;
            }
        }

        /// <summary>
        /// 移除过渡条件
        /// </summary>
        public void RemoveTransitionConditions(string targetStateName,
            Func<bool> conditions)
        {
            if (isRun)
            {
                Debug.LogError("当前状态已启动，无法移除过渡条件！");
                return;
            }
            
            if (transitions.ContainsKey(targetStateName))
            {
                //判断条件列表中是否包含该条件
                if (Tools.Tools.DelegateContainsMethod(
                    transitions[targetStateName],
                    conditions))
                {
                    transitions[targetStateName] -= conditions;
                    return;
                }
                Debug.LogWarning("条件不存在与该过渡中...");
                return;
            }
            Debug.LogWarning("过渡不存在...");
        }

        /// <summary>
        /// 删除过渡
        /// </summary>
        /// <param name="targetStateName"></param>
        public void DeleteTransition(string targetStateName)
        {
            if (isRun)
            {
                Debug.LogError("当前状态已启动，无法删除过渡！");
                return;
            }
            
            if (transitions.ContainsKey(targetStateName))
            {
                transitions.Remove(targetStateName);
            }
        }

        #endregion

        #region 判断是否满足过渡

        /// <summary>
        /// 检测过渡是否达成，返回可以过渡的状态名称
        /// </summary>
        /// <returns></returns>
        public string CheckTransition()
        {
            foreach (var transition in transitions)
            {
                //判断该过渡条件是否全部满足
                if (MeetConditions(transition.Value))
                {
                    //返回要过渡的状态名称
                    return transition.Key;
                }
            }

            return null;
        }

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <returns></returns>
        private bool MeetConditions(Func<bool> conditions)
        {
            //获取方法列表
            var list = conditions.GetInvocationList();

            //默认满足所有条件
            bool result = true;
            
            for (int i = 0; i < list.Length; i++)
            {
                //只要其中一个条件不满足，则结果为不满足
                if (!(bool)list[i].DynamicInvoke())
                    result = false;
            }
            //返回结果
            return result;
        }
        
        #endregion

        #region 状态事件

        public event Action<State> OnStateEnter;
        public Action<State> OnStateUpdate;
        public event Action<State> OnStateExit;
        
        #endregion

        #region 状态的进入与离开

        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void EnterState()
        {
            if (OnStateEnter != null)
            {
                //执行状态进入事件
                OnStateEnter(this);
            }
            
            //TODO：开启持续执行更新事件
            MonoHelper.instance.AddUpdateEventState(this);
            
            //设置已启动状态
            isRun = true;
        }

        /// <summary>
        /// 离开状态
        /// </summary>
        public virtual void ExitState()
        {
            //TODO:停止持续执行更新事件
            MonoHelper.instance.RemoveUpdateEventState(this);

            if (OnStateExit != null)
            {
                OnStateExit(this);
            }
            
            //设置未启动状态
            isRun = false;
        }

        #endregion
    }
}