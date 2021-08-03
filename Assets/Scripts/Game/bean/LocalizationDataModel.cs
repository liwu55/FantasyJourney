namespace Game.bean
{
    [System.Serializable]
    public class LocalizationDataModel
    {
        public Text[] texts;

        [System.Serializable]
        public class Text
        {
            public string[] data;
        }
    }
}