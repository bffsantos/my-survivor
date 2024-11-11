using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Spawner spawner = FindObjectOfType<Spawner>();

        spawner.SpawnPlayer();
        spawner.SpawnEnemies();
    }

    public void GameOver()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemies)
        {
            Destroy(e.gameObject);
        }

        PanelManager.Instance.ShowPanel("GameOverPanel");
    }
}
