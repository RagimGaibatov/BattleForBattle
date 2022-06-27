using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour, ISaveable{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private int sizeOfAreaSpawn;

    [SerializeField] private float timeToDestroyEnemy;

    [field: SerializeField] public int SaveId{ get; private set; }

    private List<Enemy> enemies = new List<Enemy>();

    private float timeFromLastSpawn;


    public List<Enemy> Enemies => enemies;

    [SerializeField] EnemySpawnerHealth _enemySpawnerHealth;


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


    public void AddEnemy(Enemy enemy){
        enemies.Add(enemy);
    }


    public IEnumerator ReclaimEnemy(Enemy enemy){
        enemies.Remove(enemy);
        yield return new WaitForSeconds(timeToDestroyEnemy);
        Destroy(enemy.gameObject);
    }


    public SaveData Save(){
        SaveData _saveData = new SaveData();
        _saveData.saveId = SaveId;
        _saveData.loadType = LoadType.Restore;
        _saveData.timeToSpawn = timeToSpawn;
        _saveData.health = _enemySpawnerHealth.CurrentHealth;
        return _saveData;
    }

    public void Load(SaveData data){
        timeToSpawn = data.timeToSpawn;
        _enemySpawnerHealth.CurrentHealth = data.health;
        _enemySpawnerHealth.RefreshUIHealth();
    }
}