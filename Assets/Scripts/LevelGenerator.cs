using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;
    float chunkLength = 10f;
    int checkpointInterval = 8, chunksSpawned = 0;
    
    List<GameObject> chunks = new List<GameObject>();
    
    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunk();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = moveSpeed += speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);
        
        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            cameraController.ChangeCameraFOV(speedAmount);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);
        }
    }
    
    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = SpawnPositionZ();
        GameObject chunkToGenerate = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        GameObject newChunkGO;
        
        if (chunksSpawned % checkpointInterval == 0 && chunksSpawned != 0)
        {
            newChunkGO = Instantiate(checkpointPrefab, new Vector3(0f, 0f, spawnPositionZ), Quaternion.identity, chunkParent);
        }
        else
        {
            newChunkGO = Instantiate(chunkToGenerate, new Vector3(0f, 0f, spawnPositionZ), Quaternion.identity, chunkParent);
        }
        
        chunksSpawned++;
        chunks.Add(newChunkGO);
        
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);
    }

    private float SpawnPositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    private void MoveChunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
