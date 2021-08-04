using System.Collections.Generic;
using UnityEngine;

namespace Frame.Utility
{
    public class ObjectPool : SingleTonObj<ObjectPool>
    {
        private Dictionary<string, Stack<GameObject>> objPools;

        private ObjectPool()
        {
            objPools = new Dictionary<string, Stack<GameObject>>();
        }

        /// <summary>
        /// 根据名字拿到游戏对象，负责对象的回收与复用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject SpawnObj(string name, Transform parent = null,System.Object data=null)
        {
            GameObject obj;
            //对象池里面有对应的对象可以复用
            if (objPools.ContainsKey(name) && objPools[name].Count > 0)
            {
                obj = objPools[name].Pop();
                obj.SetActive(true);
            }
            else
            {
                GameObject prefab = AssetsManager.Instance.GetPrefabByName(name);
                if (parent == null)
                {
                    obj = Object.Instantiate(prefab);
                }
                else
                {
                    obj = Object.Instantiate(prefab, parent);
                }

                obj.name = name;
            }

            PoolObject poolObject = obj.GetComponent<PoolObject>();
            Debug.Log("SpawnObj date ="+data);
            poolObject.OnSpawn(data);
            
            
            return obj;
        }

        /// <summary>
        /// 回收游戏对象
        /// </summary>
        /// <param name="obj"></param>
        public void RecycleObj(GameObject obj)
        {
            obj.SetActive(false);
            string name = obj.name;
            if (!objPools.ContainsKey(name))
            {
                Stack<GameObject> stack = new Stack<GameObject>();
                objPools.Add(name, stack);
            }

            objPools[name].Push(obj);
        }
    }
}