using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    private Player _playerReference;

    public void BindEvents(Player player)
    {
        _playerReference = player;
        _playerReference.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, FloatEventArgs e)
    {
        _healthText.text = e.value.ToString();
    }

    private void OnDisable()
    {
        _playerReference.OnHealthChanged -= Player_OnHealthChanged;
    }

}
