using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int ammo;
    [SerializeField] private float speedBullet;
    [SerializeField] private Transform spawnBulletsTransform;
    [SerializeField] private float periodAttack;
    [SerializeField] private int bulletsInOneShot;
    public Armory.GunType _gunType;


    private float time = 0;


    public void AddAmmo(int ammoToAdd){
        ammo += ammoToAdd;
    }

    private void Start(){
        time = periodAttack;
    }

    public void Shot(){
        time += Time.deltaTime;
        if (ammo < 1) return;
        if (time < periodAttack) return;


        for (int i = 0; i < bulletsInOneShot; i++){
            Bullet bullet = Instantiate(bulletPrefab, spawnBulletsTransform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * speedBullet;
        }

        ammo -= bulletsInOneShot;
        time = 0;
    }
}