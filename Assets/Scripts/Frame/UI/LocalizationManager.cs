using System;
using Frame.Utility;
using UnityEngine;

namespace Frame.UI
{
    public class LocalizationManager : SingleTonObj<LocalizationManager>
    {
        private const string LANGUAGE_ID = "launguageId";
        private event Action<int> OnLanguageChange;
        private int curLanguageId = 0;

        private LocalizationManager()
        {
            curLanguageId = PlayerPrefs.GetInt(LANGUAGE_ID, 0);
        }
        
        public void BindEvent(Action<int> languageChangeAction)
        {
            OnLanguageChange += languageChangeAction;
            //同步当前语言
            languageChangeAction(curLanguageId);
        }

        public void ChangeLanguage(int languageId)
        {
            curLanguageId = languageId;
            PlayerPrefs.SetInt(LANGUAGE_ID,languageId);
            OnLanguageChange?.Invoke(curLanguageId);
        }

        /// <summary>
        /// 根据名字获取当前语言对应的内容
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetCurLanguageText(string name)
        {
            return ConfigurationManager.Instance.GetLocalizationTextByName(name,curLanguageId);
        }
    }
}
