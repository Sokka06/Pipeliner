using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int InitialHealth = 100;
    public int MaxHealth = 100;
    public int MinHealth = 0;
    
    public int CurrentHealth { get; private set; }
    public float CurrentHealthPercentage => Mathf.Clamp01((float)CurrentHealth / (float)MaxHealth);

    public event Action<int> onHealthChanged;
    public event Action<int> onHealthMin;
    public event Action<int> onHealthMax;

    private void Awake()
    {
        SetHealth(InitialHealth);
    }

    public void SetHealth(int value, bool notify = true)
    {
        CurrentHealth = Mathf.Clamp(value, MinHealth, MaxHealth);

        if (notify)
            OnHealthChanged();
    }

    private void OnHealthChanged()
    {
        if (CurrentHealth >= MaxHealth)
            onHealthMax?.Invoke(CurrentHealth);
        
        if (CurrentHealth <= MinHealth)
            onHealthMin?.Invoke(CurrentHealth);
        
        onHealthChanged?.Invoke(CurrentHealth);
    }
}
