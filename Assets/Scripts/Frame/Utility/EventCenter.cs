using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.Utility
{
    public enum EventType
    {
        #region Room
        ShowEquip,
        PlayerEntered,
        PlayerLeft,
        UpdatePlayerReadyState,
        JudgmentStartGame,
        #endregion


        #region DoOneFight
        GetHurt,
        GameOver
        #endregion
        
    }

    public class EventCenter :  SingleTonMonoAuto<EventCenter>
    {
        private EventCenter()
        {
            allEvent = new Dictionary<EventType, Delegate>();
        }

        /// <summary>
        /// 事件中心存储的所有事件
        /// </summary>
        private Dictionary<EventType, Delegate> allEvent;
        
        //添加方法到事件中心
        //从事件中心移除方法
        //调用事件中心的某个方法

        /// <summary>
        /// 添加事件监听前的检测
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="dlg"></param>
        /// <returns></returns>
        private bool AddListenerCheck(EventType eventType,Delegate dlg)
        {
            if (!allEvent.ContainsKey(eventType))
            {
                //添加该类型的事件
                allEvent.Add(eventType,null);
            }
            else
            {
                //判断类型一致性【如果不一致】
                if (allEvent[eventType] != null && allEvent[eventType].GetType() != dlg.GetType())
                {
                    Debug.LogError("新的方法与事件的委托类型不一致！");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 添加事件【无参数无返回值】监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void AddListener(EventType eventType,Action action)
        {
            //检测
            if (!AddListenerCheck(eventType, action))
                return;
            //添加事件到字典
            allEvent[eventType] = (Action)allEvent[eventType] + action;
        }
        
        /// <summary>
        /// 添加事件【1个参数无返回值】监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void AddListener<T>(EventType eventType,Action<T> action)
        {
            //检测
            if (!AddListenerCheck(eventType, action))
                return;
            //添加事件到字典
            allEvent[eventType] = (Action<T>)allEvent[eventType] + action;
        }
        
        /// <summary>
        /// 添加事件【2个参数无返回值】监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="X"></typeparam>
        public void AddListener<T,X>(EventType eventType,Action<T,X> action)
        {
            //检测
            if (!AddListenerCheck(eventType, action))
                return;
            //添加事件到字典
            allEvent[eventType] = (Action<T,X>)allEvent[eventType] + action;
        }
        
        /// <summary>
        /// 添加事件【3个参数无返回值】监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Y"></typeparam>
        public void AddListener<T,X,Y>(EventType eventType,Action<T,X,Y> action)
        {
            //检测
            if (!AddListenerCheck(eventType, action))
                return;
            //添加事件到字典
            allEvent[eventType] = (Action<T,X,Y>)allEvent[eventType] + action;
        }
        
        /// <summary>
        /// 添加事件【4个参数无返回值】监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <typeparam name="Z"></typeparam>
        public void AddListener<T,X,Y,Z>(EventType eventType,Action<T,X,Y,Z> action)
        {
            //检测
            if (!AddListenerCheck(eventType, action))
                return;
            //添加事件到字典
            allEvent[eventType] = (Action<T,X,Y,Z>)allEvent[eventType] + action;
        }

        /// <summary>
        /// 移除监听前的检测
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="dlg"></param>
        /// <returns></returns>
        private bool RemoveListenerCheck(EventType eventType,Delegate dlg)
        {
            if (!allEvent.ContainsKey(eventType))
            {
                Debug.LogError("无该类型的事件存在，无法移除！");
                return false;
            }

            if (allEvent[eventType] == null)
            {
                Debug.LogError("该类型的事件内容为空，无法移除！");
                return false;
            }

            if (allEvent[eventType].GetType() != dlg.GetType())
            {
                Debug.LogError("事件类型与方法类型不匹配，无法移除！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 移除事件监听【无参数无返回值】
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void RemoveListener(EventType eventType,Action action)
        {
            if(!RemoveListenerCheck(eventType,action))
                return;
            allEvent[eventType] = (Action)allEvent[eventType] - action;
        }
        
        /// <summary>
        /// 移除事件监听【1个参数无返回值】
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void RemoveListener<T>(EventType eventType,Action<T> action)
        {
            if(!RemoveListenerCheck(eventType,action))
                return;
            allEvent[eventType] = (Action<T>)allEvent[eventType] - action;
        }
        
        /// <summary>
        /// 移除事件监听【2个参数无返回值】
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        public void RemoveListener<T,Y>(EventType eventType,Action<T,Y> action)
        {
            if(!RemoveListenerCheck(eventType,action))
                return;
            allEvent[eventType] = (Action<T,Y>)allEvent[eventType] - action;
        }
        
        /// <summary>
        /// 移除事件监听【3个参数无返回值】
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <typeparam name="X"></typeparam>
        public void RemoveListener<T,Y,X>(EventType eventType,Action<T,Y,X> action)
        {
            if(!RemoveListenerCheck(eventType,action))
                return;
            allEvent[eventType] = (Action<T,Y,X>)allEvent[eventType] - action;
        }
        
        /// <summary>
        /// 移除事件监听【4个参数无返回值】
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Z"></typeparam>
        public void RemoveListener<T,Y,X,Z>(EventType eventType,Action<T,Y,X,Z> action)
        {
            if(!RemoveListenerCheck(eventType,action))
                return;
            allEvent[eventType] = (Action<T,Y,X,Z>)allEvent[eventType] - action;
        }

        /// <summary>
        /// 调用前的检测
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private bool CallCheck(EventType eventType)
        {
            if (!allEvent.ContainsKey(eventType))
            {
                Debug.LogError("无该类型方法存在，无法调用！");
                return false;
            }

            if (allEvent[eventType] == null)
            {
                Debug.LogError("该类型方法为空，无法调用！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 调用没有参数的事件
        /// </summary>
        /// <param name="eventType"></param>
        public void Call(EventType eventType)
        {
            if(!CallCheck(eventType))
                return;
            //将委托转换成Action类型，并调用
            ((Action)allEvent[eventType])();
        }
        
        /// <summary>
        /// 调用有1个参数的事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="p0"></param>
        /// <typeparam name="T"></typeparam>
        public void Call<T>(EventType eventType,T p0)
        {
            if(!CallCheck(eventType))
                return;
            //将委托转换成Action类型，并调用
            ((Action<T>)allEvent[eventType])(p0);
        }
        
        /// <summary>
        /// 调用有2个参数的事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="X"></typeparam>
        public void Call<T,X>(EventType eventType,T p0,X p1)
        {
            if(!CallCheck(eventType))
                return;
            //将委托转换成Action类型，并调用
            ((Action<T,X>)allEvent[eventType])(p0,p1);
        }
        
        /// <summary>
        /// 调用有3个参数的事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Y"></typeparam>
        public void Call<T,X,Y>(EventType eventType,T p0,X p1,Y p2)
        {
            if(!CallCheck(eventType))
                return;
            //将委托转换成Action类型，并调用
            ((Action<T,X,Y>)allEvent[eventType])(p0,p1,p2);
        }
        
        /// <summary>
        /// 调用有4个参数的事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <typeparam name="Z"></typeparam>
        public void Call<T,X,Y,Z>(EventType eventType,T p0,X p1,Y p2,Z p3)
        {
            if(!CallCheck(eventType))
                return;
            //将委托转换成Action类型，并调用
            ((Action<T,X,Y,Z>)allEvent[eventType])(p0,p1,p2,p3);
        }

        /// <summary>
        /// 清除所有的事件
        /// </summary>
        public void CleanAllEvents()
        {
            allEvent.Clear();
        }

        /// <summary>
        /// 清除某个类型的所有事件
        /// </summary>
        /// <param name="eventType"></param>
        public void CleanEventType(EventType eventType)
        {
            if (allEvent.ContainsKey(eventType))
            {
                allEvent.Remove(eventType);
            }
        }

        public void Clear()
        {
            allEvent.Clear();
        }
    }
}