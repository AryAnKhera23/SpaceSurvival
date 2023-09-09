using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnner : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;
    [SerializeField] float delayInAsteroidSpawn = 1.5f;
    [SerializeField] Vector2 forceRange;

    [SerializeField] float timer;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;    
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            SpawnAsteroid();
            // reset timer
            timer = delayInAsteroidSpawn;
        }
    }

    private void SpawnAsteroid()
    {
        int side = UnityEngine.Random.Range(0, 4);

        Vector2 spawnPoint =  Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch(side) 
        {
            case 0://LeftSideOfScreen
                spawnPoint.x = 0;
                spawnPoint.y = UnityEngine.Random.value;
                direction = new Vector2(1f, UnityEngine.Random.Range(-1, 1));
                break;
            case 1://RightSideOfScreen
                spawnPoint.x = 1;
                spawnPoint.y = UnityEngine.Random.value;
                direction = new Vector2(-1f, UnityEngine.Random.Range(-1, 1));
                break;
            case 2://TopSideOfScreen
                spawnPoint.x = UnityEngine.Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(UnityEngine.Random.Range(-1, 1), -1f);
                break;
            case 3://BottomSideOfScreen
                spawnPoint.x = UnityEngine.Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(UnityEngine.Random.Range(-1, 1), 1f);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;
        Quaternion asteroidRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));

        GameObject selectRandomAsteroid = asteroids[UnityEngine.Random.Range(0, asteroids.Length)];
        GameObject asteroidInstance = Instantiate(selectRandomAsteroid, worldSpawnPoint, asteroidRotation);

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * UnityEngine.Random.Range(forceRange.x, forceRange.y);
    }
}
