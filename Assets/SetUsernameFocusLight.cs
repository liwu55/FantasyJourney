using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetUsernameFocusLight : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    private Image FocusLigthUsername;
    

    private void Start()
    {
        FocusLigthUsername = transform.Find("FocusLightUsername#").GetComponent<Image>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        FocusLigthUsername.gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        FocusLigthUsername.gameObject.SetActive(false);
    }
}
