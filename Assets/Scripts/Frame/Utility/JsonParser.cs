using UnityEngine;

namespace Frame.Utility
{
    /// <summary>
    /// 解析Json类文件
    /// </summary>
    public class JsonParser : SingleTonObj<JsonParser>
    {
        private JsonParser(){}
        public T ParseJsonFile<T>(string path)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            return JsonUtility.FromJson<T>(textAsset.text);
        }
    }
}