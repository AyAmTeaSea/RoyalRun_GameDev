using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fence;
    [SerializeField] GameObject apple;
    [SerializeField] GameObject coin;

    [SerializeField] private float appleSpawnChance = 0.33f;
    [SerializeField] private float coinSpawnChance = 0.5f;
    [SerializeField] private float coinSeperationLength = 2f;
    
    [SerializeField] float[] lanes = {-2.7f, 0f, 2.7f};
    List<int> availableLanes = new List<int> {0, 1, 2};
    
    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    
    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    void SpawnCoins()
    {
        if (availableLanes.Count <= 0) return;
        if (Random.value > coinSpawnChance) return;
        
        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn);
        
        float topOfChunkPositionZ = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPostionZ = topOfChunkPositionZ - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPostionZ);
            Coin newCoin = Instantiate(coin, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreManager);
        }
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);
        
        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;
            
            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fence, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        if (availableLanes.Count <= 0) return;
        if (Random.value > appleSpawnChance) return;
        
        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(apple, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }
    
    int SelectLane()
    {
        int randomLane = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLane];
        availableLanes.RemoveAt(randomLane);
        return selectedLane;
    }
}
