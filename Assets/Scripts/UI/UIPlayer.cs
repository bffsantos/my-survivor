using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private PlayerEventsScriptableObject _playerEvent;

    public void OnEnable()
    {
        if (_playerEvent != null)
            _playerEvent.OnHealthChanged += Player_OnHealthChanged;
    }

    private void OnDisable()
    {
        Player playerReference = FindObjectOfType<Player>();

        if (_playerEvent != null)
            _playerEvent.OnHealthChanged -= Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, FloatEventArgs e)
    {
        _healthText.text = e.value.ToString();
    }
}
