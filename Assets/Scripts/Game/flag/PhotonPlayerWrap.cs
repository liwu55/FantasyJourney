using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Game.flag
{
    public class PhotonPlayerWrap
    {
        private Player photonPlayer;
        public const string TEAM = "team";

        public PhotonPlayerWrap(Player photonPlayer)
        {
            this.photonPlayer = photonPlayer;
        }

        public void SetTeam(string teamName)
        {
            Hashtable ps = new Hashtable();
            ps[TEAM] = teamName;
            photonPlayer.SetCustomProperties(ps);
        }

        public string GetTeam()
        {
            Hashtable ps = photonPlayer.CustomProperties;
            if (ps.ContainsKey(TEAM))
            {
                return (string) ps[TEAM];
            }
            return "";
        }
    }
}