using System.Collections;
using DG.Tweening;
using Frame.UI;
using Frame.Utility;
using UnityEngine;
using UnityEngine.UI;
using EventType = Frame.Utility.EventType;

public class GameTimer : UIModuleBase 
{
    public int totalTime = 80;
    private string minuteNum;
    private string secondsNum;
    private Text txtTimer;
    public static GameTimer instance;
    private bool isShow;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
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
             else if (totalTime<70&&totalTime>=60)
             {
                 secondsNum = "0"+(totalTime-60);
             }
             else
             {
                 secondsNum = totalTime.ToString();
             }
         }
         else
         {
             secondsNum = totalTime >=60 ? "0"+(totalTime-60) : "0"+totalTime;
             if (totalTime <= 0)
             {
                 totalTime = 0;
                 //调用游戏结束事件
                 if (!isShow)
                 {
                     EventCenter.Instance.Call(EventType.GameOver);
                     isShow = true;
                 }
                
             }
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



