using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    private Slider loadingSlider;
    private GameObject goLoading;
    private Text showName;
    private void Awake()
    {
        goLoading= transform.Find("Loading").gameObject;
        showName= transform.Find("Name").GetComponent<Text>();
        loadingSlider = goLoading.GetComponent<Slider>();
    }

    public void ShowProgress(float progress)
    {
        loadingSlider.value = progress;
    }

    public bool IsShowLoading()
    {
        return goLoading.activeSelf;
    }

    public void ShowLoading()
    {
        goLoading.SetActive(true);
    }

    public void HideLoading()
    {
        goLoading.SetActive(false);
    }

    public void ShowName()
    {
        
    }

    public void SetNameColor(Color color)
    {
        showName.color = color;
    }

    public void SetHeroName(string name)
    {
        showName.text = name;
    }
}
