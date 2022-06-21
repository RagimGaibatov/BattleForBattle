using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private int sizeOfAreaSpawn;

    private List<Enemy> enemies = new List<Enemy>();

    private float time;

    public List<Enemy> Enemies => enemies;


    private void Update(){
        time += Time.deltaTime;
        if (time >= timeToSpawn){
            float xPosition = transform.position.x + Random.Range(-sizeOfAreaSpawn / 2f, sizeOfAreaSpawn / 2f);
            float zPosition = transform.position.z + Random.Range(-sizeOfAreaSpawn / 2f, sizeOfAreaSpawn / 2f);
            Vector3 spawnPos = new Vector3(xPosition, transform.position.y, zPosition);
            Enemy enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            enemy.transform.SetParent(transform);

            enemy.OriginSpawner = this;
            AddEnemy(enemy);
            time = 0;
        }
    }


    void AddEnemy(Enemy enemy){
        enemies.Add(enemy);
    }

    public void ReclaimEnemy(Enemy enemy){
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}