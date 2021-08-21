using Photon.Pun;
using UnityEngine;

namespace Game
{
    public interface IHeroController
    {
        public PhotonView GetPhotonView();
        public Transform GetTransform();
        public float GetLifeCur();
    }
}