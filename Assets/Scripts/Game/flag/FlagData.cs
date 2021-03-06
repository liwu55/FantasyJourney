using System;
using System.Collections.Generic;
using Frame.SingleTon;
using Frame.Utility;
using Game.flag;
using Photon.Pun;
using UnityEngine;

public class FlagData : SingleTonMonoPhoton<FlagData>
{
    private FlagConfiguration flagConfig;
    private List<TeamData> teamDatas;
    public Action<int, int> OnScoreChange;
    public Action<string> OnDamageShowChange;
    public Action<string> Reward;
    [NonSerialized]
    public bool gameOver = false;

    private TeamData winTeam;

    public void Awake()
    {
        base.Awake();
        teamDatas = new List<TeamData>();
        flagConfig = ConfigurationManager.Instance.GetFlagConfig();
        for (int i = 0; i < flagConfig.Teams.Length; i++)
        {
            FlagConfiguration.Team team = flagConfig.Teams[i];
            TeamData teamData = new TeamData();
            teamData.name = team.name;
            teamData.sign = team.pointName;
            teamData.color = team.color;
            teamDatas.Add(teamData);
        }
    }

    [PunRPC]
    public void OnDamageChange(string show)
    {
        OnDamageShowChange(show);
    }

    [PunRPC]
    public void OnReward(string reward)
    {
        Reward(reward);
    }
    
    [PunRPC]
    public void GotScore(string sign)
    {
        foreach (var teamData in teamDatas)
        {
            if (teamData.sign == sign)
            {
                teamData.score++;
                break;
            }
        }

        //是否有队伍到3分
        foreach (TeamData t in teamDatas)
        {
            if (t.score >= flagConfig.winScore)
            {
                gameOver = true;
                winTeam = t;
                Invoke("calc",1);
                break;
            }
        }
        
        if(OnScoreChange!=null){
            OnScoreChange(teamDatas[0].score,teamDatas[1].score);
        }
    }

    private void calc()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            FlagShowData.Instance.CalcReward(winTeam);
        }
    }
    
    public string GetFirstTeam()
    {
        return teamDatas[0].name;
    }

    public string GetSecondTeam()
    {
        return teamDatas[1].name;
    }

    public class TeamData
    {
        public string name;
        public int score;
        public string sign;
        public string color;
    }

    public Color GetColor(string teamName)
    {
        foreach (var teamData in teamDatas)
        {
            if (teamData.name == teamName)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(teamData.color, out color))
                {
                    return color;
                }
            }
        }
        return Color.white;
    }

    public string GetColorStr(string teamName)
    {
        foreach (var teamData in teamDatas)
        {
            if (teamData.name == teamName)
            {
                return teamData.color;
            }
        }
        return "#ffffff";
    }

    public Color GetAdversaryColor(string teamName)
    {
        foreach (var teamData in teamDatas)
        {
            if (teamData.name != teamName)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(teamData.color, out color))
                {
                    return color;
                }
            }
        }
        return Color.white;
    }

    public string GetUnoccupiedSignName()
    {
        return flagConfig.unoccupiedPoint;
    }

    public bool IsFirst(string teamName)
    {
        return teamDatas[0].name == teamName;
    }

    public string GetPointSign(string teamName)
    {
        foreach (var teamData in teamDatas)
        {
            if (teamData.name == teamName)
            {
                return teamData.sign;
            }
        }
        return null;
    }

    public int GetWinScore()
    {
        return flagConfig.winScore;
    }

    public void Clear()
    {
        gameOver = false;
        winTeam = null;
        FlagShowData.Instance.Clear();
    }
}
