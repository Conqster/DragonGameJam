using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [Header("Object Prefab")]
    [SerializeField] private Transform dragonPrefab;

    [Header("Spawn Locations")]
    [SerializeField] private Transform spawnPtA;
    [SerializeField] private Transform spawnPtB;

    [Header("Spawner Properties")]
    [SerializeField, Range(0.0f, 5.0f)] private float intervalsBtwSpawn = 0.5f;
    [SerializeField, Range(0.0f, 10.0f)] private float spawnWaveInteraval = 2f;
    [SerializeField, Range(0.0f, 5.0f)] private int minDragonPerSpawn = 2;
    [SerializeField, Range(0.0f, 5.0f)] private int maxDragonPerSpawn = 3;
    
    //private float spawnInterval = 0.0f;
    private int spawnIndex = 0;
    private int currentSpawnCount;

    [Header("Debuggger")]
    [SerializeField] public bool isSpawning = false;
    public float waveInterval = 0.0f;



    private void Update()
    {

        if(!isSpawning)
        {
            waveInterval += Time.deltaTime;

            if (waveInterval > spawnWaveInteraval)
            {
                currentSpawnCount = Random.Range(minDragonPerSpawn, maxDragonPerSpawn + 1);

                StartCoroutine(StartSpawn());
                waveInterval = 0.0f;
            }

        }

    }


    private IEnumerator StartSpawn()
    {
        isSpawning = true;

        for (int i = 0; i < currentSpawnCount; i++)
        {
            SpawnDecision();
            yield return new WaitForSeconds(intervalsBtwSpawn);
        }

        isSpawning = false;
    }

    private void SpawnDecision()
    {
        if(spawnIndex == 0)
            Spawn(spawnPtA.position, spawnPtA.rotation);
        else
            Spawn(spawnPtB.position, spawnPtB.rotation);    

        spawnIndex = (spawnIndex + 1) % 2;
    }





    private void Spawn(Vector3 pos, Quaternion rot)
    {
        //int spawnIndex = Random.Range(0, 2);

        Instantiate(dragonPrefab, pos, rot);
    }



}
