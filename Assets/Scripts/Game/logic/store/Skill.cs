using System;
using UnityEngine.EventSystems;

public class Skill : EventTrigger
{
    public Action OnEnter;
    public Action OnExit;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (OnEnter != null)
        {
            OnEnter();
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (OnExit != null)
        {
            OnExit();
        }
    }
}
