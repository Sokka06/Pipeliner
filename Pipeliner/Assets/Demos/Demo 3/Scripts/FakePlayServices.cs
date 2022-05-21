using System;
using System.Collections;
using System.Collections.Generic;
using Demos.Common;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace Demos.Demo3
{
    public class Data
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }
    
    public class Support
    {
        public string url { get; set; }
        public string text { get; set; }
    }
    
    public class FakeUserData
    {
        public Data data { get; set; }
        public Support support { get; set; }
    }
    
    /// <summary>
    /// Loads fake user data from API.
    /// </summary>
    public class FakePlayServices : SingletonBehaviour<FakePlayServices>
    {
        public bool IsInitialized { get; private set; }
        public FakeUserData Data { get; private set; }
        
        private const string URL = "https://reqres.in/api/users";
    
        public void Initialize()
        {
            StartCoroutine(InitializeService());
        }
    
        public IEnumerator InitializeService()
        {
            var delay = "?delay=3";
            using (UnityWebRequest request = UnityWebRequest.Get($"{URL}/{Random.Range(1, 7)}{delay}"))
            {
                var operation = request.SendWebRequest();
                while (!operation.isDone)
                {
                    yield return null;
                }
                
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                    Data = JsonConvert.DeserializeObject<FakeUserData>(request.downloadHandler.text);
                }
            }
            
            IsInitialized = true;
        }
    }
}