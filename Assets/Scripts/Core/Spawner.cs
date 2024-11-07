using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _playerPrefab;
    
    private Transform player;

    public Vector2 spawnArea;

    public float spawnInterval;

    public void SpawnPlayer()
    {
        GameObject playerGameObject = Instantiate(_playerPrefab);
        player = playerGameObject.transform;
    }

    public void SpawnEnemies()
    {
        StartCoroutine(SpawnCoroutine(spawnInterval));
    }

    private IEnumerator SpawnCoroutine(float interval)
    {
        while (true)
        {
            Vector2 randomPosition = GenerateRandomPosition();

            randomPosition += (Vector2)player.position;

            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.transform.position = randomPosition;
            enemy.GetComponent<Enemy>().target = player;

            yield return new WaitForSeconds(interval);
        }
    }

    private Vector2 GenerateRandomPosition()
    {
        Vector2 position = new Vector2();

        float value = Random.value > 0.5 ? -1 : 1;

        if(Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * value;
        }
        else
        {
            position.y = Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * value;
        }

        return position;
    }
}
