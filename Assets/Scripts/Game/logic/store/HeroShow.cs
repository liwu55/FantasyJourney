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

    public Animator ShowHero(string hero)
    {
        RecycleAll();
        GameObject goHero = ObjectPool.Instance.SpawnObj(hero, heroesTrans);
        //旋转237度
        goHero.transform.rotation=Quaternion.Euler(0,237,0);
        Animator animator = goHero.GetComponent<Animator>();
        if (animator == null)
        {
            animator = goHero.GetComponentInChildren<Animator>();
        }
        return animator;
    }

    private void RecycleAll()
    {
        foreach (Transform t in heroesTrans)
        {
            ObjectPool.Instance.RecycleObj(t.gameObject);
        }
    }
}
