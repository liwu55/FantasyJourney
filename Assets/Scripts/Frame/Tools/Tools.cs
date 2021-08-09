using System;
using UnityEngine;

namespace Frame.Tools
{
    public static class Tools
    {
        /// <summary>
        /// 判断某个方法是否包含于某个委托中
        /// </summary>
        /// <param name="dlg"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool DelegateContainsMethod(Delegate dlg,Delegate method)
        {
            //获取到委托的所有方法
            var list = dlg.GetInvocationList();

            //遍历所有方法
            for (int i = 0; i < list.Length; i++)
            {
                if (method == list[i])
                {
                    Debug.LogWarning("委托中包含该方法...");
                    return true;
                }
            }

            return false;
        }
    }
}