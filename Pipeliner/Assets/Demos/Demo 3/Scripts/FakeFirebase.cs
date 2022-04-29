using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Datum
{
    public int id { get; set; }
    public string name { get; set; }
    public int year { get; set; }
    public string color { get; set; }
    public string pantone_value { get; set; }
}

public class FakeItemData
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public List<Datum> data { get; set; }
    public Support support { get; set; }
}

/// <summary>
/// Loads fake item data from API.
/// </summary>
public class FakeFirebase : SingletonBehaviour<FakeFirebase>
{
    public bool IsInitialized { get; private set; }
    public FakeItemData Data { get; private set; }
    
    private const string URL = "https://reqres.in/api/unknown";

    public void Initialize()
    {
        StartCoroutine(InitializeService());
    }

    private IEnumerator InitializeService()
    {
        var delay = "?delay=3";
        using (UnityWebRequest request = UnityWebRequest.Get($"{URL}{delay}"))
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
                Data = JsonConvert.DeserializeObject<FakeItemData>(request.downloadHandler.text);
            }
        }
        
        IsInitialized = true;
    }
}
