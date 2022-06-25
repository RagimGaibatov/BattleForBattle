using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] private int damage;
    private Rigidbody rb;

    public PoolOfBullets Origin;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out EnemyHealth enemyHealth)){
            enemyHealth.TakeDamage(damage);
            Origin.Reclaim(this);
            return;
        }

        if (other.TryGetComponent(out EnemySpawnerHealth enemySpawnerHealth)){
            enemySpawnerHealth.TakeDamage(damage);
            Origin.Reclaim(this);
        }
    }


    public void Init(Vector3 velocity, Vector3 position){
        transform.position = position;
        rb.velocity = velocity;
        transform.rotation = Quaternion.LookRotation(velocity);
    }
}