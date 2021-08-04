using Frame.Utility;
using UnityEngine;

public class HeroShow : SingleTonMono<HeroShow>
{
    private Transform heroesTrans;
    protected override void Awake()
    {
        base.Awake();
        heroesTrans = transform.Find("Heroes");
    }

    public void ShowHero(string hero)
    {
        RecycleAll();
        ObjectPool.Instance.SpawnObj(hero, heroesTrans);
    }

    private void RecycleAll()
    {
        foreach (Transform t in heroesTrans)
        {
            ObjectPool.Instance.RecycleObj(t.gameObject);
        }
    }
}
