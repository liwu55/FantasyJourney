using UnityEngine;

/// <summary>
/// 自动生成对象，可以直接使用
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMonoAuto<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = typeof(T).Name;
                instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
}
