using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool gameOver;

    public event EventHandler OnGameStarted;
    public event EventHandler OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        gameOver = false;

        Spawner spawner = FindObjectOfType<Spawner>();

        spawner.SpawnPlayer();
        spawner.SpawnEnemies();

        OnGameStarted?.Invoke(this, EventArgs.Empty);
    }

    public void GameOver()
    {
        gameOver = true;

        OnGameOver?.Invoke(this, EventArgs.Empty);

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemies)
        {
            Destroy(e.gameObject);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
