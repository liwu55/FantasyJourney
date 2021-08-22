using System;
using UnityEngine;

/// <summary>
/// 普通单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingleTonObj<T> where T :class
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //Debug.Log("获取单例初始化，name="+typeof(T).Name);
                instance = Activator.CreateInstance(typeof(T),true) as T;
            }
            return instance;
        }
    }
}
