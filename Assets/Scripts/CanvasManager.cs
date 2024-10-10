using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CanvasManager : MonoBehaviour
{
    public Text healthText;

    public GameObject titleScreen;
    public GameObject playerScreen;

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
    }

    public void UpdateHealth(float health)
    {
        healthText.text = health.ToString();
    }
    private void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }
}
