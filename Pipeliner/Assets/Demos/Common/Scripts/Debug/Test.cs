using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string Name;
    
    private int _frame;

    private void Awake()
    {
        Debug.Log($"{Name}: Awake");
    }

    private void Start()
    {
        Debug.Log($"{Name}: Start");
    }

    private void Update()
    {
        Debug.Log($"{Name}: {_frame}");
        _frame++;
    }
}
