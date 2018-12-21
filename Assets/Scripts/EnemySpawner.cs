﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    //[SerializeField] float TimeBetweenWaves = 10;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Use this for initialization
    IEnumerator Start () {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
        
	}

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            if(waveIndex == waveConfigs.Count - 1)
            {
                StartCoroutine(WaitAndLoad());
            }
        }
        
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Win Condition");


    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate
                (waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }
}