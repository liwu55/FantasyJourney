using System;

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
                instance = Activator.CreateInstance(typeof(T),true) as T;
            }
            return instance;
        }
    }
}
