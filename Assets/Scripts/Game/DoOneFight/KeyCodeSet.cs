using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCodeSet : MonoBehaviour
{
    public static KeyCodeSet _instance;
    public static KeyCodeSet Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new KeyCodeSet();
            }
            return _instance;
        }
        
    }
    private bool SumActive;
    private Text ChangingKey;
    public IEnumerator KeyChange()
    {
        while (true)  //等待按键
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keycode))
                    {
                        /*if ( InputMgr.Instance.inputDic.ContainsKey(()Enum.Parse(ChangingKey.text) as ))
                        {
                            InputMgr.Instance.inputDic[ChangingKey.text] = keycode; //通过名字改按键字典
                        }*/
                    }
                }
                break;
            }
            yield return null;
        }
    }
   
}
