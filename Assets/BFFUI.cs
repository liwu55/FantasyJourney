using System;
using System.Collections;
using System.Collections.Generic;
using Frame.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class BFFUI : UIModuleBase
{
    private Text time;
    public int curTime;
    public int AllTime = 60;
    private Button backButton;
    protected override void Awake()
    {
        base.Awake();
        backButton = transform.Find("BFFGameOverPanel/BackButton").GetComponent<Button>();
    }
    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
           // UIManager.Instance.Clear();
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("SampleScene");
        });
        time = transform.Find("Text").GetComponent<Text>();
        curTime = (int)Time.time;
    }

    private void Update()
    {
        if ((int) Time.time - curTime > 1)
        {
            AllTime--;
            curTime = (int) Time.time;
            time.text = AllTime.ToString();

            if (AllTime <= 0)
            {
                Time.timeScale = 0;
                UIManager.Instance.ShowModule("BFFGameOverPanel");
                transform.Find("BFFGameOverPanel").gameObject.SetActive(true);
            }
        }

    }
}
