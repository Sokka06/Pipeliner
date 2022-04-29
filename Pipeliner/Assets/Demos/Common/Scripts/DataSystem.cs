using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Demos.Common
{
    public interface IData
    {
        string FileName { get; set; }
    }
    
    /// <summary>
    /// A simple class for saving data to a JSON file on disk.
    /// NOTE: Change Application.dataPath to Application.persistentDataPath in GetSavePath() if you want to use this outside editor.
    /// </summary>
    public class DataSystem : MonoBehaviour
    {
        /// <summary>
        /// Loaded data.
        /// </summary>
        public Dictionary<string, IData> Data { get; private set; } = new Dictionary<string, IData>();
        
        public const string FOLDER_NAME = "SaveData";
        public const string FORMAT_NAME = ".json";

        /// <summary>
        /// Loads data from file and returns it.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>() where T : IData
        {
            //Check if already loaded
            var key = GetFileName<T>();
            if (IsLoaded<T>())
            {
                Debug.LogWarning($"Attempted to load data of type {key} which is already loaded with the {GetType().Name}.");
                return Get<T>();
            }
            
            var data = Deserialize<T>();
            
            //Add to list of loaded data.
            Data.Add(key, data);
            return data;
        }
        
        /// <summary>
        /// Saves data to file.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public void Save<T>(T data) where T : IData
        {
            var key = GetFileName<T>();
            if (!IsLoaded<T>())
            {
                Debug.LogWarning($"Attempted to save data of type {key} which is not yet loaded with the {GetType().Name}.");
                return;
            }

            Data[key] = data;
            Serialize((T)Data[key]);
        }

        /// <summary>
        /// Unloads data from loaded data.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public void Unload<T>() where T : IData
        {
            var key = GetFileName<T>();
            if (!IsLoaded<T>())
            {
                Debug.LogWarning($"Attempted to unload data of type {key} which is not yet loaded with the {GetType().Name}.");
                return;
            }

            Data.Remove(key);
        }

        /// <summary>
        /// Gets loaded data. Be sure to load data before using.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Get<T>() where T : IData
        {
            var key = GetFileName<T>();
            if (!IsLoaded<T>())
            {
                Debug.LogWarning($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }
            
            return (T)Data[key];
        }

        /// <summary>
        /// Is data loaded.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsLoaded<T>() where T : IData
        {
            return Data.ContainsKey(GetFileName<T>());
        }

        /// <summary>
        /// Checks if data exists on disk.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Exists<T>() where T : IData
        {
            var fileName = GetFileName<T>();
            var filePath = GetFullSavePath(fileName);

            return File.Exists(filePath);
        }
        
        public string GetSavePath()
        {
            return $"{Application.dataPath}/{FOLDER_NAME}/";
        }

        public string GetFullSavePath (string fileName)
        {
            return GetSavePath() + fileName + FORMAT_NAME;
        }

        #region private data handling
        /// <summary>
        /// Serializes class to file.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        private void Serialize<T>(T data) where T : IData
        {
            var path = GetSavePath();
            var fileName = GetFileName<T>() + FORMAT_NAME;
        
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path + fileName, json);
        }
        
        /// <summary>
        /// Deserializes class from file.
        /// </summary>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T Deserialize<T>() where T : IData
        {
            var fileName = GetFileName<T>();
            var filePath = GetFullSavePath(fileName);
        
            if (!File.Exists(filePath))
            {
                Debug.LogWarning("No data found for '" + GetFileName<T>() + "', returned empty data!");
                return default;
            }

            var json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<T>(json);
            
            //Restore filename as it does not get saved to file and it is not given by the constructor.
            //obviously not necessary when not using filename from IData interface.
            data.FileName = fileName;
            return data;
        }

        private void Delete<T>() where T : IData
        {
            var filePath = GetFullSavePath(GetFileName<T>());
        
            if (!File.Exists(filePath))
            {
                Debug.LogWarning("File System: Can't Delete '" + GetFileName<T>() + "', Because No Data Was Found!");
                return;
            }

            File.Delete(filePath);
        }
        #endregion

        private string GetFileName<T>() where T : IData
        {
            var data = default(T);
            //Use class name if no file name is set.

            return string.IsNullOrEmpty(data.FileName) ? typeof(T).Name : data.FileName;
        }
    }
}