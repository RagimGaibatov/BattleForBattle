using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour{
    [SerializeField] private int health;

    public int Health{
        get => health;
        set => health = value;
    }


    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0){
            GetComponent<Enemy>().Die();
        }
    }
}