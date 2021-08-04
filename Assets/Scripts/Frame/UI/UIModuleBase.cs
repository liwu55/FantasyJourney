using System.Collections.Generic;
using Frame.Constant;
using Frame.Utility;
using UnityEngine;

namespace Frame.UI
{
    public enum ModuleShowType
    {
        Single,
        Multiple
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class UIModuleBase : MonoBehaviour,PoolObject
    {
        private Dictionary<string, UIWidget> _uiWidgets;
        public ModuleShowType moduleShowType;
        private CanvasGroup cg;
        protected virtual void Awake()
        {
            _uiWidgets=new Dictionary<string, UIWidget>();
            //根据标记找到重要的对象
            FindImportantWidget();
            cg = GetComponent<CanvasGroup>();
        }

        private void FindImportantWidget()
        {
            //通过拿Transform拿到所有子类
            Transform[] trans = GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < trans.Length; i++)
            {
                Transform childTran = trans[i];
                if (CheckSuffix(childTran))
                {
                    UIWidget uiWidget = childTran.gameObject.AddComponent<UIWidget>();
                    _uiWidgets.Add(childTran.name,uiWidget);
                }
            }
        }

        /// <summary>
        /// 通过名字找到对象上面的UIWidget管理脚本对象
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public UIWidget FW(string objName)
        {
            if (_uiWidgets.ContainsKey(objName))
            {
                return _uiWidgets[objName];
            }
            return null;
        }

        private bool CheckSuffix(Transform childTran)
        {
            foreach (string keyChar in SystemDefine.IMPORTANT_OBJ_SUFFIX)
            {
                if (childTran.gameObject.name.EndsWith(keyChar))
                {
                    return true;
                }
            }
            return false;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnSpawn()
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
        }

        public void OnRecycle()
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
        }

        public void OnPause()
        {
            cg.blocksRaycasts = false;
        }

        public void OnResume()
        {
            cg.blocksRaycasts = true;
        }
    }
}
