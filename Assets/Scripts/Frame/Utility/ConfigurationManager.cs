using System.Collections.Generic;
using Frame.Constant;
using Game.bean;

namespace Frame.Utility
{
    /// <summary>
    /// 负责配置的管理
    /// </summary>
    public class ConfigurationManager : SingleTonObj<ConfigurationManager>
    {
        private ConfigurationManager()
        {
        }

        private Dictionary<string, string> _assetsPaths;
        private Dictionary<string, string[]> _localizationTextData;
        private List<HeroInfos.Hero> heros;

        public List<HeroInfos.Hero> GetHeroInfos()
        {
            if (heros == null)
            {
                InitHeros();
            }

            return heros;
        }

        private void InitHeros()
        {
            heros=new List<HeroInfos.Hero>();
            HeroInfos heroInfos = JsonParser.Instance.ParseJsonFile<HeroInfos>(
                SystemDefine.PATH_CONFIGURATION_HEROS);
            for (int i = 0; i < heroInfos.heros.Length; i++)
            {
                heros.Add(heroInfos.heros[i]);
            }
        }

        /// <summary>
        /// 根据名字拿到路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetPathByName(string name)
        {
            if (_assetsPaths == null)
            {
                InitAssetsPath();
            }
            if (_assetsPaths.ContainsKey(name))
            {
                return _assetsPaths[name];
            }
            return null;
        }

        /// <summary>
        /// 加载资源配置文件
        /// </summary>
        private void InitAssetsPath()
        {
            _assetsPaths=new Dictionary<string, string>();
            AssetsPathModel assetsPathModel = JsonParser.Instance
                .ParseJsonFile<AssetsPathModel>(
                SystemDefine.PATH_CONFIGURATION_ASSETS_PATH);
            for (int i = 0; i < assetsPathModel.paths.Length; i++)
            {
                _assetsPaths.Add(assetsPathModel.paths[i].name, 
                    assetsPathModel.paths[i].path);
            }
        }

        public string GetLocalizationTextByName(string name,int languageId)
        {
            if (_localizationTextData == null)
            {
                InitLanguageTextData();
            }
            if(_localizationTextData.ContainsKey(name)){
                return _localizationTextData[name][languageId];
            }
            return "";
        }

        private void InitLanguageTextData()
        {
            _localizationTextData=new Dictionary<string, string[]>();
            LocalizationDataModel localizationDataModel = JsonParser.Instance.
                ParseJsonFile<LocalizationDataModel>(SystemDefine.
                    PATH_CONFIGURATION_LOCALIZATION_TEXT_DATA);
            LocalizationDataModel.Text[] texts = localizationDataModel.texts;
            for (int i = 0; i < texts.Length; i++)
            {
                string[] data = texts[i].data;
                _localizationTextData.Add(data[0],data);
            }
        }
    }
}