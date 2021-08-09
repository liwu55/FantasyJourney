using Frame.UI;
using Frame.Utility;
using Game.bean;
using UnityEngine;

public class HeroItemModule : UIModuleBase
{
    private StoreHeroInfo storeHero;
    private GameObject lockObj;
    private GameObject chooseObj;

    public void BindInfo(StoreHeroInfo storeHero)
    {
        this.storeHero = storeHero;
        FW("Avatar#").Img.sprite=ResManager.LoadImg(storeHero.hero.avatar);
        lockObj = FW("Lock#").gameObject;
        lockObj.SetActive(!storeHero.owned);
        chooseObj = FW("Choose#").gameObject;
        chooseObj.SetActive(storeHero.choose);
    }

    public void NotifyInfoChange()
    {
        lockObj.SetActive(!storeHero.owned);
        chooseObj.SetActive(storeHero.choose);
    }
}
