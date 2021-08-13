using ExitGames.Client.Photon;
using Frame.SingleTon;
using Photon.Realtime;
public enum EProperty
{
    team,
    
}

namespace Game.DoOneFight.Init
{
    public class CustomPlayerTeam : SingleTonMonoPhoton<CustomPlayerTeam>
    {
        private string teamName;
        private Player _player;
        public CustomPlayerTeam(Player player)
        {
            _player = player;
        }

        public void SetTeam(string teamName)
        {
            Hashtable teamProperty = new Hashtable();
            teamProperty[EProperty.team] = teamName;
            _player.SetCustomProperties(teamProperty);
        }
        public string GetTeamName()
        {
            return (string)_player.CustomProperties[EProperty.team];
        }
    }
}