using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private FloatGameEvent _healthEvent;

    public void OnEnable()
    {
        if (_healthEvent != null)

            _healthEvent.gameEvent += OnHealthChanged;
    }

    private void OnDisable()
    {
        if (_healthEvent != null)
            _healthEvent.gameEvent -= OnHealthChanged;
    }

    private void OnHealthChanged(float value)
    {
        _healthText.text = value.ToString();
    }
}
