[System.Serializable]
public class FlagConfiguration
{
    public int winScore;
    public string unoccupiedPoint;
    public float occupyTime = 1;
    public Team[] Teams;

    [System.Serializable]
    public class Team
    {
        public string name;
        public string color;
        public string pointName;
    }
}