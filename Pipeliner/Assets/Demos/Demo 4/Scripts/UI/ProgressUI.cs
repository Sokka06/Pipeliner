using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    public Slider ProgressBar;

    private PlayerController _player;
    private Start _start;
    private Finish _finish;

    private float _totalDistance;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _start = FindObjectOfType<Start>();
        _finish = FindObjectOfType<Finish>();

        _totalDistance = Vector2.Distance(_start.transform.position, _finish.transform.position);
    }

    private void LateUpdate()
    {
        var currentDistance = Vector2.Distance(_player.Vehicle.transform.position, _finish.transform.position);
        ProgressBar.value = Mathf.Clamp01(1f - currentDistance / _totalDistance);
    }
}
