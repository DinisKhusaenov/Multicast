using System.IO;
using Infrastructure.Serialization;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
    public class DataLocalProvider : IDataProvider
    {
        private const string FileName = "GameSave";
        private const string SaveFileExtension = ".json";
        
        private string SavePath => Application.persistentDataPath;
        private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");
        
        public void Save<T>(T data) where T : ISaveData
        {
            File.WriteAllText(FullPath, data.ToJson());
        }

        public T Load<T>() where T : ISaveData, new()
        {
            if (IsDataAlreadyExist() == false)
                return new T();

            return File.ReadAllText(FullPath).FromJson<T>();
        }

        private bool IsDataAlreadyExist() => File.Exists(FullPath);
    }
}