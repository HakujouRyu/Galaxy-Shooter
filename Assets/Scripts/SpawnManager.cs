using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _spawnRate = 2f;

    [SerializeField]
    private float _powerupSpawnRate = 10f;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawn = false;

    void Awake()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawn == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawn == false)
        {
            int choice = Random.Range(0,3);

            GameObject newPowerup = Instantiate(powerups[choice]);
            yield return new WaitForSeconds(_powerupSpawnRate);

        }

    }


    public void OnPlayerDeath()
    {
        _stopSpawn = true;
    }
}
