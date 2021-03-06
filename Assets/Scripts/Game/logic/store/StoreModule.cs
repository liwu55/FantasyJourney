using System.Collections;
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
    private Button back;
    private Text actionText;
    private StoreHeroInfo checkingHero;
    private StoreHeroInfo chooseHero;
    private UIWidget skillShow;
    private Text skillName;
    private Text skillDes;
    private Text money;
    private Text honor;

    private void Start()
    {
        InitViews();
        ShowAllHeroes();
        InitListener();
    }

    private void InitViews()
    {
        _heroManager = HeroManager.Instance;
        heroListTrans = FW("HeroList#").transform;
        tg = FW("HeroList#").Tg;
        heroName = FW("name#").Text;
        skill1 = FW("Skill1#");
        skill2 = FW("Skill2#");
        actionButton = FW("Action#").Button;
        actionText = FW("ActionText#").Text;
        skillShow = FW("SkillShow#");
        skillName = FW("SkillName#").Text;
        skillDes = FW("SkillDes#").Text;
        back = FW("BackButton#").Button;
        money = FW("Money#").Text;
        honor = FW("Honor#").Text;
        ShowMoney();
        HideSkillDes();
    }

    private void ShowMoney()
    {
        money.text = PlayerInfo.Instance._userInfo.money.ToString();
        honor.text = PlayerInfo.Instance._userInfo.honor.ToString();
    }

    private void InitListener()
    {
        back.onClick.AddListener(() =>
        {
            UIEvent.RefreshMainPageHero();
            UIManager.Instance.PopModule();
        });
        skill1.Button.onClick.AddListener(() => { ShowSkill1(); });
        skill2.Button.onClick.AddListener(() => { ShowSkill2(); });
        skill1.GetComponent<Skill>().OnEnter += () => { ShowSkillDes(checkingHero.hero.skills[0]); };
        skill1.GetComponent<Skill>().OnExit += () => { HideSkillDes(); };
        skill2.GetComponent<Skill>().OnEnter += () => { ShowSkillDes(checkingHero.hero.skills[1]); };
        skill2.GetComponent<Skill>().OnExit += () => { HideSkillDes(); };

        actionButton.onClick.AddListener(() =>
        {
            if (checkingHero == null)
            {
                return;
            }

            //??????
            if (!checkingHero.owned)
            {
                if (PlayerInfo.Instance._userInfo.money < checkingHero.hero.price)
                {
                    return;
                }

                PlayerInfo.Instance._userInfo.money -= checkingHero.hero.price;
                PlayerInfo.Instance._userInfo.BuyHero(checkingHero.hero.id);
                ShowMoney();
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
                    //??????????????????false
                    chooseHero.choose = false;
                    SyncIconStatus(chooseHero);
                    checkingHero.choose = true;
                    chooseHero = checkingHero;
                    SyncIconStatus(checkingHero);
                    SyncShowHeroInfo(null);
                    PlayerInfo.Instance.SetChooseHero(chooseHero.hero.id);
                }
            }
        });
    }

    private void ShowSkill2()
    {
        if (checkingHero.hero.id < 3)
        {
            ShowSkillAction("Skill2");
        }
        else
        {
            checkingHero.animator.SetInteger("Action",1002);
            Invoke("ResetAction",0.5f);
        }
    }

    private void ShowSkill1()
    {
        if (checkingHero.hero.id < 3)
        {
            ShowSkillAction("Skill1");
        }
        else
        {
            checkingHero.animator.SetInteger("Action",1001);
            Invoke("ResetAction",.5f);
        }
    }

    private void ResetAction()
    {
        checkingHero.animator.SetInteger("Action",0);
    }
    private void ShowSkillAction(string skillName)
    {
        checkingHero.animator.SetBool(skillName, true);
    }

    private void HideSkillDes()
    {
        skillShow.gameObject.SetActive(false);
    }

    private void ShowSkillDes(HeroInfos.Hero.Skill heroSkill)
    {
        skillShow.gameObject.SetActive(true);
        //Debug.Log("skillName="+skillName);
        //Debug.Log("heroSkill="+heroSkill);
        skillName.text = heroSkill.name;
        skillDes.text = heroSkill.des;
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
            //??????
            if (PlayerInfo.Instance.IsChoose(hero.id))
            {
                storeHeroInfo.choose = true;
                chooseHero = storeHeroInfo;
            }

            heroItemModule.BindInfo(storeHeroInfo);
            itemToggle.group = tg;
            //??????????????????????????????????????????
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
        return PlayerInfo.Instance._userInfo.CheckIfHas(hero.id);
    }

    void SyncShowHeroInfo(StoreHeroInfo storeHero)
    {
        bool isHeroChange = checkingHero == null ||
                            (storeHero != null && checkingHero != null
                                               && checkingHero.hero != storeHero.hero);

        if (storeHero != null)
        {
            checkingHero = storeHero;
        }

        if (isHeroChange)
        {
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
                actionText.text = "?????????";
            }
            else
            {
                actionText.text = "??????";
            }
        }
        else
        {
            actionText.text = "??????(" + checkingHero.hero.price + ")";
        }
    }

    private void SyncIconStatus(StoreHeroInfo storeHero)
    {
        Transform child = heroListTrans.GetChild(storeHero.index);
        child.GetComponent<HeroItemModule>().NotifyInfoChange();
    }
}