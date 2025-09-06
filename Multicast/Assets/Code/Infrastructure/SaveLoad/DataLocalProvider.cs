using System.IO;
using Infrastructure.Serialization;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
    public class DataLocalProvider : IDataProvider
    {
        private const string FileName = "GameSave";
        private const string SaveFileExtension = ".json";

        private IPersistentData _persistentData;

        public DataLocalProvider(IPersistentData persistentData)
        {
            _persistentData = persistentData;
        }
        
        private string SavePath => Application.persistentDataPath;
        private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");
        
        public void Save()
        {
            File.WriteAllText(FullPath, _persistentData.GameData.ToJson());
        }

        public bool TryLoad()
        {
            if (IsDataAlreadyExist() == false)
                return false;

            _persistentData.GameData = File.ReadAllText(FullPath).FromJson<GameData>();
            return true;
        }

        private bool IsDataAlreadyExist() => File.Exists(FullPath);
    }
}