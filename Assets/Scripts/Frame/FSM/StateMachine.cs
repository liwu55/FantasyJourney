using System.Collections.Generic;
using UnityEngine;

namespace Frame.FSM
{
    public class StateMachine : State
    {
        public StateMachine(string stateName) : base(stateName)
        {
            controlledStates = new Dictionary<string, State>();
            //将检测过渡方法绑定到更新事件
            OnStateUpdate += CheckCurrentStateTransition;
        }

        /// <summary>
        /// 被管理的状态
        /// </summary>
        private Dictionary<string, State> controlledStates;

        /// <summary>
        /// 默认状态
        /// </summary>
        private State defaultState;

        /// <summary>
        /// 当前状态
        /// </summary>
        private State currentState;

        #region 添加和移除状态

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="stateName"></param>
        public void AddState(string stateName)
        {
            AddState(new State(stateName));
        }

        public void AddState(State state)
        {
            if (currentState != null)
            {
                Debug.LogError("当前状态机已经启动，无法再添加状态！");
                return;
            }
            
            if (!controlledStates.ContainsKey(state.StateName))
            {
                controlledStates.Add(state.StateName,state);
                //如果添加的该状态为状态机中的第一个状态
                if (controlledStates.Count == 1)
                {
                    //设置该状态为默认状态
                    defaultState = state;
                }
            }
        }

        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="stateName"></param>
        public void RemoveState(string stateName)
        {
            if (currentState != null)
            {
                Debug.LogError("当前状态机已经启动，无法再移除状态！");
                return;
            }
            
            if (controlledStates.ContainsKey(stateName))
            {
                //移除
                controlledStates.Remove(stateName);
            }
        }
        
        #endregion

        #region 进入状态机和离开状态机

        public override void EnterState()
        {
            base.EnterState();

            if (defaultState != null)
            {
                //当前状态即为默认状态
                currentState = defaultState;
                //当前状态进入
                currentState.EnterState();
            }
        }

        public override void ExitState()
        {
            if (currentState != null)
            {
                currentState.ExitState();
            }

            currentState = null;

            base.ExitState();
        }

        #endregion

        #region 检测当前状态是否满足了过渡条件，进行过渡
        
        /// <summary>
        /// 检测当前状态是否过渡
        /// </summary>
        private void CheckCurrentStateTransition(State state)
        {
            if (currentState != null)
            {
                string targetState = currentState.CheckTransition();

                if (targetState != null)
                {
                    //TODO:状态过渡
                    Transition(targetState);
                }
            }
        }

        /// <summary>
        /// 状态过渡
        /// </summary>
        /// <param name="targetStateName"></param>
        private void Transition(string targetStateName)
        {
            if (!controlledStates.ContainsKey(targetStateName))
            {
                Debug.LogWarning("状态过渡绑定出现错误，请检测" 
                                 + currentState.StateName + "与"
                                 + targetStateName + "的过渡绑定！");
                return;
            }
            
            //离开此时的当前状态
            currentState.ExitState();
            //更新当前状态
            currentState = controlledStates[targetStateName];
            //让新的当前状态执行进入
            currentState.EnterState();
        }

        #endregion

        #region 查找当前状态机中的某个子状态

        /// <summary>
        /// 查找状态
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public State FindState(string stateName)
        {
            if (controlledStates.ContainsKey(stateName))
            {
                return controlledStates[stateName];
            }
            return null;
        }

        #endregion
        
    }
}