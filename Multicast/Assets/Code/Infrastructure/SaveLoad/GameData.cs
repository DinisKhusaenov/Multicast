using System;

namespace Infrastructure.SaveLoad
{
    [Serializable]
    public class GameData : ISaveData
    {
        public int Level;
    }
}