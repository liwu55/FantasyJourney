using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Frame.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : UIModuleBase
{
    private int totalTime = 10;
    private string minuteNum;
    private string secondsNum;
    private Text txtTimer;
    protected override void Awake()
    {
        base.Awake();
        txtTimer = FW("Timer#").Text;
    }

    private void Start()
    {
        StartCoroutine(TimerCountDown());
        
    }

    private IEnumerator TimerCountDown()
    {
        while (true)
        {
            totalTime -= 1;
            if (totalTime <= 15)
                ShakeText();
            yield return new WaitForSeconds(1);
        }
    }

    private void Update()
    {
         minuteNum = totalTime >= 60 ? "01" : "00";
         if (totalTime>=10)
         {
             if (totalTime>=70)
             {
                 secondsNum = totalTime >=60 ? (totalTime-60).ToString() : totalTime.ToString();
             }
             else if (totalTime<70&&totalTime>=10)
             {
                 secondsNum = totalTime.ToString();
             }
         }
         else
         {
             if (totalTime <= 0)
                 totalTime = 0;
             secondsNum = totalTime >=60 ? "0"+(totalTime-60).ToString() : "0"+totalTime.ToString();
         }
         string str = string.Format("{0}:{1}",minuteNum,secondsNum);
         txtTimer.text = str;
    }

    private void ShakeText()
    {
        Tweener doPunch = txtTimer.rectTransform.DOPunchPosition(new Vector3(0, -10, 0), 1, 8, 1);
        doPunch.SetAutoKill(false);
        doPunch.SetEase(Ease.Linear);
        doPunch.SetLoops(1);
        Tweener doColor = txtTimer.DOColor(Color.red, 2);
        doColor.SetAutoKill(false);
        doColor.SetEase(Ease.Linear);
        doColor.SetLoops(1);
    }
}



