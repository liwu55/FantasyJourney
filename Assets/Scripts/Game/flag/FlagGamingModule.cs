using Frame.UI;
using UnityEngine;
using UnityEngine.UI;

public class FlagGamingModule : UIModuleBase
{
    private Text ourPoint;
    private Text adversarysPoint;
    private bool isFirst = false;
    protected override void Awake()
    {
        base.Awake();
        ourPoint=FW("OurPoint#").Text;
        adversarysPoint=FW("AdversarysPoint#").Text;
    }

    public override void OnSpawn(object obj)
    {
        string ourTeamName = (string) obj;
        isFirst = FlagData.Instance.IsFirst(ourTeamName);
        Color ourColor = FlagData.Instance.GetColor(ourTeamName);
        Color adversarysColor = FlagData.Instance.GetAdversaryColor(ourTeamName);
        ourPoint.color = ourColor;
        adversarysPoint.color = adversarysColor;
        FlagData.Instance.OnScoreChange += ScoreChange;
    }

    private void ScoreChange(int firstTeamScore, int secondTeamScore)
    {
        if (isFirst)
        {
            ourPoint.text = firstTeamScore.ToString();
            adversarysPoint.text = secondTeamScore.ToString();
        }
        else
        {
            ourPoint.text = secondTeamScore.ToString();
            adversarysPoint.text = firstTeamScore.ToString();
        }

        string winTeam = null;
        if (firstTeamScore >= FlagData.Instance.GetWinScore())
        {
            winTeam = FlagData.Instance.GetFirstTeam();
        }
        else if(secondTeamScore >= FlagData.Instance.GetWinScore() )
        {
            winTeam = FlagData.Instance.GetSecondTeam();
        }
        if (winTeam != null)
        {
            UIManager.Instance.ShowModule("FlagOver",winTeam);
        }
    }
}
