using System;
using Frame.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas:MonoBehaviour
{
    private Text playerName;
    private Slider hpBar;
    protected void Awake()
    {
        playerName = transform.Find("Name").GetComponent<Text>();
        hpBar = transform.Find("Blood").GetComponent<Slider>();
    }

    public void SetPlayerName(string nameStr)
    {
        playerName.text = nameStr;
    }

    public void SetHpPercent(float value)
    {
        hpBar.value = value;
    }
    
    
}
