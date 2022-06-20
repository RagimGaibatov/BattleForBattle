using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    [SerializeField] private int ammo;
    [SerializeField] private float periodAttack;
    [SerializeField] private int bulletsInOneShot;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform spawnBulletsTransform;
    [SerializeField] private Bullet bulletPrefab;

    private float updateTimeToRotate = 0.2f;
    private float timeFromLastRotate = 0f;

    private PoolOfBullets _poolOfBullets;
    private EnemySpawner _enemySpawner;

    private float currentTimeFromLastShot = 0;

    private void Start(){
        _poolOfBullets = new PoolOfBullets(bulletPrefab);
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        currentTimeFromLastShot = periodAttack;
    }

    private void Update(){
        timeFromLastRotate += Time.deltaTime;
        if (timeFromLastRotate >= updateTimeToRotate){
            RotateToClosestEnemy();
            timeFromLastRotate = 0;
        }
    }

    public void Shot(){
        currentTimeFromLastShot += Time.deltaTime;
        if (ammo < 1) return;
        if (currentTimeFromLastShot < periodAttack) return;

        for (int i = 0; i < bulletsInOneShot; i++){
            Bullet bullet = _poolOfBullets.GetBullet();
            bullet.Init(spawnBulletsTransform.forward * bulletSpeed,
                spawnBulletsTransform.position + new Vector3(0f, 0f, i / 1.5f));
        }


        ammo -= bulletsInOneShot;
        currentTimeFromLastShot = 0;
    }

    public void AddAmmo(int ammoToAdd){
        ammo += ammoToAdd;
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