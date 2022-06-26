using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private int sizeOfAreaSpawn;

    [SerializeField] private float timeToDestroyEnemy;

    private List<Enemy> enemies = new List<Enemy>();

    private float timeFromLastSpawn;

    public List<Enemy> Enemies => enemies;

    private EnemySpawnerHealth _enemySpawnerHealth;

    private void Start(){
        _enemySpawnerHealth = GetComponent<EnemySpawnerHealth>();
    }


    private void Update(){
        timeFromLastSpawn += Time.deltaTime;
        if (timeFromLastSpawn >= timeToSpawn){
            float xPosition = transform.position.x + Random.Range(-sizeOfAreaSpawn / 2f, sizeOfAreaSpawn / 2f);
            float zPosition = transform.position.z + Random.Range(-sizeOfAreaSpawn / 2f, sizeOfAreaSpawn / 2f);
            Vector3 spawnPos = new Vector3(xPosition, transform.position.y, zPosition);
            if (timeToSpawn < -0.2f){
                timeToSpawn = 6f * _enemySpawnerHealth.HealthInPercentage;
            }

            Enemy enemy;
            switch (Random.value){
                case > 0.92f:
                    enemy = Instantiate(_enemyPrefabs[2], spawnPos, Quaternion.identity);
                    timeToSpawn += 0.4f;
                    break;
                case > 0.7f:
                    enemy = Instantiate(_enemyPrefabs[1], spawnPos, Quaternion.identity);
                    timeToSpawn += 0.1f;
                    break;
                default:
                    enemy = Instantiate(_enemyPrefabs[0], spawnPos, Quaternion.identity);
                    timeToSpawn -= 0.4f;
                    break;
            }

            enemy.transform.SetParent(transform);

            enemy.OriginSpawner = this;
            AddEnemy(enemy);
            timeFromLastSpawn = 0;
        }
    }


    void AddEnemy(Enemy enemy){
        enemies.Add(enemy);
    }


    public IEnumerator ReclaimEnemy(Enemy enemy){
        enemies.Remove(enemy);
        yield return new WaitForSeconds(timeToDestroyEnemy);
        Destroy(enemy.gameObject);
    }
}