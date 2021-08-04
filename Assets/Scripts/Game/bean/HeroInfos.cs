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
        public string[] skills;
        public int price;
    }
}