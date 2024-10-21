using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainGame : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject playerScreen;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStarted -= GameManager_OnGameStarted;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, EventArgs e)
    {
        playerScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        titleScreen.SetActive(false);
        playerScreen.SetActive(true);
    }
}
