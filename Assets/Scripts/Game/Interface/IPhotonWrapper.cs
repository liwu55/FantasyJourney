using System;
using System.Collections.Generic;

namespace Game.Interface
{
    /// <summary>
    /// photon网络框架的封装
    /// </summary>
    public interface IPhotonWrapper
    {
        void RandomJoin(Action<bool> result);

        void CreateRoom();

        List<RoomInfo> GetAllRooms();

        void JoinRoom(Action<bool> result);
    }
}