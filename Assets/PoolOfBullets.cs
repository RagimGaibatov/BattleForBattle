using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolOfBullets{
    private Bullet bulletPrefab;

    public PoolOfBullets(Bullet bulletPrefab){
        this.bulletPrefab = bulletPrefab;
    }

    private Queue<Bullet> pool = new Queue<Bullet>();

    public Bullet GetBullet(){
        Bullet bullet = default;
        if (pool.Count == 0){
            bullet = Object.Instantiate(bulletPrefab);
        }
        else{
            bullet = pool.Dequeue();
        }

        bullet.Origin = this;
        ShowBullet(bullet);

        return bullet;
    }

    void ShowBullet(Bullet bullet){
        bullet.gameObject.SetActive(true);
    }

    public void Reclaim(Bullet bullet){
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}