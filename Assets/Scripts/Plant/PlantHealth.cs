using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealth : MonoBehaviour{
    [SerializeField] private int health;
    [SerializeField] private Plant _plant;

    public int Health{
        get => health;
        set => health = value;
    }
    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0){
            _plant.Die();
        }
    }
}