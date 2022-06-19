using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float timeToSpawn;

    private List<Enemy> enemies = new List<Enemy>();

    private float time;

    public List<Enemy> Enemies => enemies;


    private void Update(){
        time += Time.deltaTime;
        if (time >= timeToSpawn){
            Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.OriginSpawner = this;
            AddEnemy(enemy);
            time = 0;
        }
    }


    void AddEnemy(Enemy enemy){
        enemies.Add(enemy);
    }

    public void Reclaim(Enemy enemy){
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}