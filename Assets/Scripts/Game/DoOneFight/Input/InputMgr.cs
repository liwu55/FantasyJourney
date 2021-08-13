using System.Collections.Generic;
using UnityEngine;

public enum EKeyName
{
    front = 1,
    back = 2,
    leftMove = 3,
    rightMove = 4,
    jump = 10,
    normalAttack = 100,
    heavyAttack = 101,
    Skill02 = 102
}
public class InputMgr 
{
    private static InputMgr _inputMgr;
    public static InputMgr Instance
    {
        get
        {
            if (_inputMgr==null)
            {
                _inputMgr = new InputMgr();
            }
            return _inputMgr;
        }
    }
    public Dictionary<EKeyName , KeyCode> inputDic = new Dictionary<EKeyName, KeyCode>();

    public void InitInputDic()
    {
        if (inputDic.Count == 0 )
        {
            inputDic.Add(EKeyName.front, KeyCode.W);
            inputDic.Add(EKeyName.back, KeyCode.S);
            inputDic.Add(EKeyName.leftMove, KeyCode.A);
            inputDic.Add(EKeyName.rightMove, KeyCode.D);
            inputDic.Add(EKeyName.jump, KeyCode.Space);
            inputDic.Add(EKeyName.normalAttack, KeyCode.J);
            inputDic.Add(EKeyName.heavyAttack, KeyCode.K);
            inputDic.Add(EKeyName.Skill02, KeyCode.I);
        }
    }

    public void ResetInputDic()
    {
        inputDic.Clear();
        inputDic.Add(EKeyName.front, KeyCode.W);
        inputDic.Add(EKeyName.back, KeyCode.S);
        inputDic.Add(EKeyName.leftMove,KeyCode.A);
        inputDic.Add(EKeyName.rightMove,KeyCode.D);
        inputDic.Add(EKeyName.jump,KeyCode.Space);
        inputDic.Add(EKeyName.normalAttack,KeyCode.J);
        inputDic.Add(EKeyName.Skill02, KeyCode.I);
    }

    public void SetNewInputCode(EKeyName _keyName,KeyCode _keyCode)
    {
        //字典中已经存在替换的键位
        if (inputDic.ContainsKey(_keyName))
        {
            inputDic[_keyName] = KeyCode.None;
        }
        //如果不存在
        
        if (!inputDic.ContainsKey(_keyName))
        {
            inputDic[_keyName] = _keyCode;
        }
    }
}
