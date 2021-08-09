using System;
using System.Collections;
using System.Collections.Generic;
using Frame.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : UIModuleBase
{
    private int totalTime = 90;
    private int minuteNum;
    private int secondsNum;
    private Text txtTimer;
    protected override void Awake()
    {
        base.Awake();
        txtTimer = FW("Timer#").Text;
    }

    private void Start()
    {
        throw new NotImplementedException();
    }

    private IEnumerator TimerCountDown()
    {
        totalTime -= 1;
        yield return new WaitForSeconds(1);
        
    }

    private void Update()
    {
         minuteNum = totalTime >= 60 ? 1 : 0;
         secondsNum = totalTime - 60;
         string str = string.Format("{0}:{1}",minuteNum,secondsNum);
         txtTimer.text = str;
    }
}
