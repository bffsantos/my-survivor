using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Spawner _spawner;
    
    public bool gameOver;

    public event EventHandler OnGameStarted;
    public event EventHandler OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = true;
        
        _spawner = FindObjectOfType<Spawner>();
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

    public void StartGame()
    {
        gameOver = false;

        OnGameStarted?.Invoke(this, EventArgs.Empty);

        _spawner.SpawnPlayer();
        _spawner.SpawnEnemies();
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
}
