using System.Collections.Generic;
using Frame.UI;
using Frame.Utility;
using Game;
using Game.bean;
using Game.Interface;
using UnityEngine;
using UnityEngine.UI;

public class StoreModule : UIModuleBase
{
    private IHeroManager _heroManager;
    private Transform heroListTrans;
    private ToggleGroup tg;
    private Text heroName;
    private UIWidget skill1;
    private UIWidget skill2;
    private Button actionButton;
    private Text actionText;
    private StoreHeroInfo checkingHero;
    private StoreHeroInfo chooseHero;

    protected override void Awake()
    {
        base.Awake();
        _heroManager = new HeroManager();
        heroListTrans = FW("HeroList#").transform;
        tg = FW("HeroList#").Tg;
        heroName = FW("name#").Text;
        skill1 = FW("Skill1#");
        skill2 = FW("Skill2#");
        actionButton = FW("Action#").Button;
        actionText = FW("ActionText#").Text;
    }

    private void Start()
    {
        ShowAllHeroes();
        InitListener();
    }

    private void InitListener()
    {
        skill1.Button.onClick.AddListener(() =>
        {
            checkingHero.animator.SetTrigger("skill1");
        });
        skill2.Button.onClick.AddListener(() =>
        {
            checkingHero.animator.SetTrigger("skill2");
        });
        actionButton.onClick.AddListener(() =>
        {
            if (checkingHero == null)
            {
                return;
            }

            if (!checkingHero.owned)
            {
                checkingHero.owned = true;
                SyncShowHeroInfo(checkingHero);
                SyncIconStatus(checkingHero);
            }
            else
            {
                if (checkingHero.choose)
                {
                    return;
                }
                else
                {
                    //当前选中改为false
                    chooseHero.choose = false;
                    SyncIconStatus(chooseHero);
                    checkingHero.choose = true;
                    chooseHero = checkingHero;
                    SyncIconStatus(checkingHero);
                    SyncShowHeroInfo(null);
                }

                //TODO 替换英雄
            }
        });
    }

    private void ShowAllHeroes()
    {
        List<HeroInfos.Hero> allHero = _heroManager.GetAllHero();
        for (int i = 0; i < allHero.Count; i++)
        {
            HeroInfos.Hero hero = allHero[i];
            //Debug.Log(hero);
            GameObject goItem = ObjectPool.Instance.SpawnObj("HeroIconItem", heroListTrans);
            Toggle itemToggle = goItem.GetComponent<Toggle>();
            HeroItemModule heroItemModule = goItem.GetComponent<HeroItemModule>();
            StoreHeroInfo storeHeroInfo = new StoreHeroInfo();
            storeHeroInfo.hero = hero;
            storeHeroInfo.owned = CheckIfOwn(hero);
            storeHeroInfo.index = i;
            if (i == 1)
            {
                storeHeroInfo.choose = true;
                chooseHero = storeHeroInfo;
            }

            heroItemModule.BindInfo(storeHeroInfo);
            itemToggle.group = tg;
            //第一个设为选中，显示当前信息
            if (i == 0)
            {
                itemToggle.isOn = true;
                SyncShowHeroInfo(storeHeroInfo);
            }

            itemToggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    SyncShowHeroInfo(storeHeroInfo);
                }
            });
        }
    }

    private bool CheckIfOwn(HeroInfos.Hero hero)
    {
        return hero.id != 0;
    }

    void SyncShowHeroInfo(StoreHeroInfo storeHero)
    {
        bool isHeroChange = checkingHero == null || 
                            (storeHero != null && checkingHero != null
                                               && checkingHero.hero != storeHero.hero);

        if(storeHero!=null){
            checkingHero = storeHero;
        }

        if(isHeroChange){
            HeroInfos.Hero hero = checkingHero.hero;
            heroName.text = hero.name;
            checkingHero.animator = HeroShow.Instance.ShowHero(hero.model);
            skill1.Img.sprite = ResManager.LoadImg(hero.skills[0].icon);
            skill2.Img.sprite = ResManager.LoadImg(hero.skills[1].icon);
        }
        
        if (checkingHero.owned)
        {
            if (checkingHero == chooseHero)
            {
                actionText.text = "已选择";
            }
            else
            {
                actionText.text = "使用";
            }
        }
        else
        {
            actionText.text = "购买";
        }
    }

    private void SyncIconStatus(StoreHeroInfo storeHero)
    {
        Transform child = heroListTrans.GetChild(storeHero.index);
        child.GetComponent<HeroItemModule>().NotifyInfoChange();
    }
}