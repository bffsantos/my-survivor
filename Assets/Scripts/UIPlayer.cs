using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    public void OnEnable()
    {
        Player playerReference = FindObjectOfType<Player>();

        if (playerReference != null)
            playerReference.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, FloatEventArgs e)
    {
        _healthText.text = e.value.ToString();
    }

    private void OnDisable()
    {
        Player playerReference = FindObjectOfType<Player>();

        if (playerReference != null)
            playerReference.OnHealthChanged -= Player_OnHealthChanged;
    }

}
