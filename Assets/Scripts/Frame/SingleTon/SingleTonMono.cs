using UnityEngine;

/// <summary>
/// 需要手动添加到一个GameObject上再使用
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingleTonMono<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}

