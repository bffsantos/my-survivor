using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasManager canvasManager;
    public Spawner spawner;
    
    public Player player;
    
    public bool gameOver;

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

    public void StartGame()
    {
        gameOver = false;

        canvasManager.titleScreen.SetActive(false);
        canvasManager.playerScreen.SetActive(true);

        canvasManager.UpdateHealth(100);
        
        player.health = 100;
        player.gameObject.SetActive(true);

        spawner.SpawnEnemies();
    }

    public void GameOver()
    {
        gameOver = true;

        canvasManager.titleScreen.SetActive(true);
        canvasManager.playerScreen.SetActive(false);

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemies)
        {
            Destroy(e.gameObject);
        }
    }
}
