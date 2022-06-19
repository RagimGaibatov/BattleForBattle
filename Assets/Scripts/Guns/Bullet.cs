using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
    private float time;
    private Rigidbody rb;

    public PoolOfBullets Origin;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    private void Update(){
        time += Time.deltaTime;
        if (time > lifeTime){
            Origin.Reclaim(this);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out Enemy enemy)){
            if (other.TryGetComponent(out Health health)){
                health.TakeDamage(damage);
            }
        }
    }

    public void Init(Vector3 velocity, Vector3 position){
        time = 0;
        transform.position = position;
        rb.velocity = velocity;
        transform.forward = velocity;
    }
}