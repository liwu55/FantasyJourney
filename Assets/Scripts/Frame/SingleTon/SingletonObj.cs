using UnityEngine;

/// <summary>
/// 普通单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonObj<T> where T : new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
