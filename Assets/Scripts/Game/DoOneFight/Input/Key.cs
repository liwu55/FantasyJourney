using System;
using UnityEngine;

[Serializable]
public class Key
{
    public string name;
    public KeyCode keyCode;
    public KeyCodeType keyType = KeyCodeType.Once;
    [HideInInspector]
    public bool IsDown = false;
    [HideInInspector]
    public bool enable = true;
    
}
public enum KeyCodeType
{
    Once,
    Continuity
}
[Serializable]
public class ValueKey
{
    public string name;
    public Vector2 range=new Vector2(0,1);
    public float currValue=0;
    public float addSpeed = 3f;
    public KeyCode keyCode;
    [HideInInspector]
    public bool enable = true;
}
[Serializable]
public class AxisKey
{
    public string name;
    public float value=0;
    public float addSpeed=5;
    public Vector2 range=new Vector2(-1,1);
    public KeyCode min, max;
    [HideInInspector]
    public bool enable = true;
    public void SetKey(KeyCode a,KeyCode b)
    {
        min = a;
        max = b;
    }
}