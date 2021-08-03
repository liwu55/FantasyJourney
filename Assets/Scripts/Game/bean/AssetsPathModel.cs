namespace Game.bean
{
    [System.Serializable]
    public class AssetsPathModel
    {
        public Path[] paths;

        [System.Serializable]
        public class Path
        {
            public string name;
            public string path;
        }
    }
}