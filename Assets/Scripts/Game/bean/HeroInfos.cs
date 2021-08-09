[System.Serializable]
public class HeroInfos
{
    public Hero[] heros;

    [System.Serializable]
    public class Hero
    {
        public int id;
        public string name;
        public string des;
        public string model;
        public string avatar;
        public Skill[] skills;
        public int price;

        [System.Serializable]
        public class Skill
        {
            public string icon;
            public string name;
            public string des;
        }
    }
}