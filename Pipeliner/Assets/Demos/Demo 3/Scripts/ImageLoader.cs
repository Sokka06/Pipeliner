using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public RawImage Image;
    
    public Texture2D Texture2D { get; private set; }

    public event Action<Texture2D> onImageLoaded;

    private void OnEnable()
    {
        onImageLoaded += SetImage;
    }

    private void OnDisable()
    {
        onImageLoaded -= SetImage;
    }

    private void OnDestroy()
    {
        if (Image != null)
            Destroy(Image);
    }

    public void SetImage(Texture2D image)
    {
        Image.texture = Texture2D;
    }

    public void LoadImage(string url)
    {
        StartCoroutine(LoadImage(url, OnImageLoaded));
    }

    private void OnImageLoaded(Texture2D image)
    {
        Texture2D = image;
        onImageLoaded?.Invoke(Texture2D);
    }

    private IEnumerator LoadImage(string url, Action<Texture2D> result)
    {
        using var request = UnityWebRequestTexture.GetTexture(url);
        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            yield return null;
        }
            
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            result?.Invoke(null);
        }
        else
        {
            result?.Invoke(((DownloadHandlerTexture) request.downloadHandler).texture);
        }
    }
}
