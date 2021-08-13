using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputData : ScriptableObject
{
    public List<Key> keys=new List<Key>() { new Key() };
    public List<ValueKey> valueKeys=new List<ValueKey>() { new ValueKey()};
    public List<AxisKey> axisKeys=new List<AxisKey>() {new AxisKey()};

    public void SetKey(string name,KeyCode key)
    {
        Key key1= GetKey(name);
        if (key1!=null)
        {
            key1.keyCode = key;
        }
    }
    public void SetAxisKey(string name,KeyCode a,KeyCode b)
    {
        AxisKey axisKey = GetAxisKey(name);
        if (axisKey != null)
        {
            axisKey.SetKey(a,b);
        }
    }

    public void SetValueKey(string name,KeyCode key)
    {
        ValueKey valueKey = GetValueKey(name);
        if (valueKey!=null)
        {
            valueKey.keyCode = key;
        }
    }
    public ValueKey GetValueKey(string name)
    {
        int len = valueKeys.Count;
        for (int i = 0; i < len; i++)
        {
            if (valueKeys[i].name==name)
            {
                return valueKeys[i];
            }
        }
        Debug.LogError("ValueKey:" + name + "不存在");
        return null;
    }
    public AxisKey GetAxisKey(string name)
    {
        int len = axisKeys.Count;
        for (int i = 0; i < len; i++)
        {
            if (axisKeys[i].name == name)
            {

                return axisKeys[i];
            }
        }
        Debug.LogError("AxisKey:" + name + "不存在");
        return null;
    }
    public Key GetKey(string name)
    {
        int len = keys.Count;
        for (int i = 0; i < len; i++)
        {
            if (keys[i].name==name)
            {
                return keys[i];
            }
        }
        Debug.LogError("Key:"+name+"不存在");
        return null;
    }
    public float Axis(string name)
    {
        AxisKey axisKey = GetAxisKey(name);
        if (axisKey != null)
        {
            return axisKey.value;
        }
        return 0;
    }
    public bool GetKeyDown(string name)
    {
        Key key = GetKey(name);
        if (key != null)
        {
            return key.IsDown;
        }
        return false;
    }
    public float GetValue(string name)
    {
        ValueKey valueKey = GetValueKey(name);
        if (valueKey != null)
        {
            return valueKey.currValue;
        }
        return 0;
    }
    public void SetKeyEnable(string name,bool enable)
    {
        Key key = GetKey(name);
        if (key!=null)
        {
            key.enable = enable;
            key.IsDown = false;
        }
    }

    public void SetValueKeyEnable(string name,bool enable)
    {
        ValueKey valueKey = GetValueKey(name);
        if (valueKey!=null)
        {
            valueKey.enable = enable;
            valueKey.currValue = 0;
        }
    }
    public void SetAxisKeyEnable(string name,bool enable)
    {
        AxisKey axisKey = GetAxisKey(name);
        if (axisKey!=null)
        {
            axisKey.enable = enable;
            axisKey.value = 0;
        }
    }
    /// <summary>
    /// 每帧更新
    /// </summary>
    public void AcceptInput()
    {
        UpdateKeys();
        UpdateValueKey();
        UpdateAllAxisKey();
    }
    private void UpdateKeys()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].enable)
            {
                keys[i].IsDown = false;
                switch (keys[i].keyType)
                {
                    case KeyCodeType.Once:
                        if (Input.GetKeyDown(keys[i].keyCode))
                        {
                            keys[i].IsDown = true;
                        }
                        break;
                    case KeyCodeType.Continuity:
                        if (Input.GetKey(keys[i].keyCode))
                        {
                            keys[i].IsDown = true;
                        }
                        break;
                }
            }
        }
    }
    private void UpdateValueKey()
    {
        int len = valueKeys.Count;
        for (int i = 0; i < len; i++)
        {
            if (valueKeys[i].enable)
            {
                if (Input.GetKey(valueKeys[i].keyCode))
                {
                    valueKeys[i].currValue = Mathf.Clamp(valueKeys[i].currValue + valueKeys[i].addSpeed * Time.deltaTime, valueKeys[i].range.x, valueKeys[i].range.y);
                }
                else
                {
                    valueKeys[i].currValue = Mathf.Clamp(valueKeys[i].currValue - valueKeys[i].addSpeed * Time.deltaTime, valueKeys[i].range.x, valueKeys[i].range.y);
                }

            }

        }
    }
    private void UpdateAllAxisKey()
    {
        int len = axisKeys.Count;
        for (int i = 0; i < len; i++)
        {
            UpdateAxisKey(axisKeys[i]);

        }
    }
    private void UpdateAxisKey(AxisKey axisKey)
    {
        if (!axisKey.enable)
            return;
        if (Input.GetKey(axisKey.min) || Input.GetKey(axisKey.max))
        {
            if (Input.GetKey(axisKey.min))
                axisKey.value = Mathf.Clamp(axisKey.value - axisKey.addSpeed * Time.deltaTime, axisKey.range.x, axisKey.range.y);
            if (Input.GetKey(axisKey.max))
                axisKey.value = Mathf.Clamp(axisKey.value + axisKey.addSpeed * Time.deltaTime, axisKey.range.x, axisKey.range.y);
        }
        else if (Input.GetKey(axisKey.min) && Input.GetKey(axisKey.max))
        {
            axisKey.value = 0;
        }
        else
        {
            axisKey.value = Mathf.Lerp(axisKey.value, 0, Time.deltaTime * axisKey.addSpeed);
        }
    }
}