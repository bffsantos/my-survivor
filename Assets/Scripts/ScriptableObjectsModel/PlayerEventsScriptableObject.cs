using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEvent", menuName = "ScriptableObjects/Scriptable Event")]
public class PlayerEventsScriptableObject : ScriptableObject
{
    public event EventHandler<FloatEventArgs> OnHealthChanged;

    public void HealthChanged(float newValue)
    {
        OnHealthChanged?.Invoke(this, new FloatEventArgs { value = newValue });
    }

}
