using System;
using UnityEngine;
using UnityEngine.UI;

namespace Frame.UI
{
    public class LocalizationText : MonoBehaviour
    {
        private string textName;
        private Text text;
        private void Awake()
        {
            text = GetComponent<Text>();
            textName= text.text;
            LocalizationManager.Instance.BindEvent(ChangeLanguage);
        }

        private void ChangeLanguage(int languageId)
        {
            text.text = LocalizationManager.Instance.GetCurLanguageText(textName);
        }
        
    }
}