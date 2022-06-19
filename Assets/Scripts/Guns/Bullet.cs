using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [SerializeField] private int damage;


    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out Enemy enemy)){
            enemy.Health.TakeDamage(damage);
        }
    }

    public void Init(float speed){
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}