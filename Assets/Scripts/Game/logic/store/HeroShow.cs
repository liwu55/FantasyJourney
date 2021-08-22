using Frame.Utility;
using Game.DoOneFight.State;
using UnityEngine;

public class HeroShow : SingleTonMono<HeroShow>
{
    private Transform heroesTrans;

    protected override void Awake()
    {
        base.Awake();
        heroesTrans = transform.Find("Heroes");
    }

    public Animator ShowHero(string hero)
    {
        RecycleAll();
        GameObject goHero = ObjectPool.Instance.SpawnObj(hero, heroesTrans);
        //旋转237度
        goHero.transform.rotation = Quaternion.Euler(0, 237, 0);
        if (!HasController(goHero))
        {
            goHero.AddComponent<SimpleHeroController>();
        }

        Animator animator = goHero.GetComponent<Animator>();
        if (animator == null)
        {
            animator = goHero.GetComponentInChildren<Animator>();
        }

        return animator;
    }

    private bool HasController(GameObject goHero)
    {
        return goHero.GetComponent<SimpleHeroController>() != null
               || goHero.GetComponent<PlayerCrtlr>() != null;
    }

    private void RecycleAll()
    {
        foreach (Transform t in heroesTrans)
        {
            ObjectPool.Instance.RecycleObj(t.gameObject);
        }
    }

    public void Clear()
    {
        Instance = null;
    }
}