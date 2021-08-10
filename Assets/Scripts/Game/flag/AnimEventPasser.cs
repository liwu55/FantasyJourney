using UnityEngine;

public class AnimEventPasser : MonoBehaviour
{
    public SimpleHeroController target;
    
    private void Skill1End()
    {
        target.Skill1End();
    }

    private void Skill2End()
    {
        target.Skill2End();
    }
}
