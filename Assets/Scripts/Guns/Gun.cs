using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int ammo;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform spawnBulletsTransform;
    [SerializeField] private float periodAttack;
    [SerializeField] private int bulletsInOneShot;

    private EnemySpawner _enemySpawner;


    private float time = 0;


    public void AddAmmo(int ammoToAdd){
        ammo += ammoToAdd;
    }

    private void Start(){
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        time = periodAttack;
    }

    public void Shot(){
        time += Time.deltaTime;

        if (ammo < 1) return;
        if (time < periodAttack) return;

        RotateToClosestEnemy();

        for (int i = 0; i < bulletsInOneShot; i++){
            Bullet bullet = Instantiate(bulletPrefab, spawnBulletsTransform.position + new Vector3(0f, 0f, i / 2f),
                spawnBulletsTransform.rotation);
            bullet.Init(bulletSpeed);
        }

        ammo -= bulletsInOneShot;
        time = 0;
    }

    void RotateToClosestEnemy(){
        if (_enemySpawner.Enemies.Count == 0){
            return;
        }

        Vector3 directionToClosestEnemy = Vector3.positiveInfinity;
        float length = Single.PositiveInfinity;

        for (int i = 0; i < _enemySpawner.Enemies.Count; i++){
            Vector3 direction = (_enemySpawner.Enemies[i].transform.position - transform.position);
            if (length > direction.sqrMagnitude){
                directionToClosestEnemy = direction;
                length = directionToClosestEnemy.sqrMagnitude;
            }
        }

        transform.rotation = Quaternion.LookRotation(directionToClosestEnemy);
    }
}