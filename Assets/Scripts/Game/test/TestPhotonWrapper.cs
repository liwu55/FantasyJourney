using System;
using System.Collections.Generic;
using Game.bean;
using Game.Interface;

namespace Game
{
    public class TestPhotonWrapper:IPhotonWrapper
    {
        public void RandomJoin(Action<bool> result)
        {
        }

        public void CreateRoom()
        {
        }

        public List<RoomInfo> GetAllRooms()
        {
            return new List<RoomInfo>();
        }

        public void JoinRoom(Action<bool> result)
        {
        }

        public List<RoomPlayerInfo> GetCurRoomPlayers()
        {
            return new List<RoomPlayerInfo>();
        }
    }
}