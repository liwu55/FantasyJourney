using Frame.UI;
using UnityEngine;
using UnityEngine.UI;

public class FlagOverModule : UIModuleBase
{
    private Text winTeamShow;
    protected override void Awake()
    {
        base.Awake();
        winTeamShow=FW("WinTeamShow#").Text;
    }

    public override void OnSpawn(object obj)
    {
        string teamName = (string) obj;
        winTeamShow.color = FlagData.Instance.GetColor(teamName);
        winTeamShow.text = teamName +"获胜";
    }
}
