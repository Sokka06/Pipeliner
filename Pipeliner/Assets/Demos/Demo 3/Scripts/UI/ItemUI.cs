using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image Background;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI YearText;
    
    [Space]
    public TextMeshProUGUI LikeCount;
    public Button LikeButton;

    private Datum _data;
    
    public int Count { get; private set; }
    public Datum Data
    {
        get => _data;
        set
        {
            _data = value;
            SetName(_data.name);
            SetYear(_data.year);
            SetColor(_data.color);
        }
    }

    public event Action<(int id, int count)> onCountChanged;

    private void Awake()
    {
        SetCount(0);
        LikeButton.onClick.AddListener(OnLikeButtonClicked);
    }

    private void OnDestroy()
    {
        LikeButton.onClick.RemoveListener(OnLikeButtonClicked);
    }

    private void OnLikeButtonClicked()
    {
        SetCount(Count + 1);
    }

    private void SetName(string name)
    {
        NameText.SetText(Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name));
    }
    
    private void SetYear(int year)
    {
        YearText.SetText(year.ToString());
    }

    private void SetColor(string color)
    {
        ColorUtility.TryParseHtmlString(color, out var colorRgb);
        Background.color = colorRgb;
    }

    public void SetCount(int count)
    {
        Count = count;
        LikeCount.SetText(Count.ToString());
        onCountChanged?.Invoke((Data.id, Count));
    }
}
