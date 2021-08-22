using System.Collections.Generic;
using UnityEngine;

namespace Frame.Utility
{
    public class AssetsManager : SingleTonObj<AssetsManager>
    {
        private Dictionary<string, GameObject> _prefabsCache;

        private AssetsManager()
        {
        }

        /// <summary>
        /// 根据名字拿到预制体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetPrefabByName(string name)
        {
            if (_prefabsCache == null)
            {
                _prefabsCache = new Dictionary<string, GameObject>();
            }

            if (_prefabsCache.ContainsKey(name))
            {
                return _prefabsCache[name];
            }

            GameObject prefab = Resources.Load<GameObject>(GetPathByName(name));
            //_prefabsCache.Add(name, prefab);
            return prefab;
        }

        private string GetPathByName(string name)
        {
            return ConfigurationManager.Instance.GetPathByName(name);
        }
    }
}