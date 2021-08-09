using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetFocusPasswordLight : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    private Image FocusLigthPassword;
    void Start()
    {
        FocusLigthPassword = transform.Find("FocusLightPassword#").GetComponent<Image>();
    }


    public void OnSelect(BaseEventData eventData)
    {
        FocusLigthPassword.gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        FocusLigthPassword.gameObject.SetActive(false);
    }
}
