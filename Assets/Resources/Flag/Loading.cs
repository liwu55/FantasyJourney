using System;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private Slider s;
    private void Awake()
    {
        s = transform.Find("Slider").GetComponent<Slider>();
    }

    public void ShowProgress(float progress)
    {
        s.value = progress;
    }
}
