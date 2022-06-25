using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerHealth : MonoBehaviour{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;


    private EnemySpawner _enemySpawner;

    [SerializeField] private Image winScreen;
    [SerializeField] private RectTransform backgroundMaxHealth;
    [SerializeField] private RectTransform foregroundCurrentHealth;

    private void Start(){
        _enemySpawner = GetComponent<EnemySpawner>();
        currentHealth = maxHealth;
        RefreshUIHealth();
    }


    public void TakeDamage(int damage){
        currentHealth -= damage;
        RefreshUIHealth();
        if (currentHealth <= 0){
            winScreen.gameObject.SetActive(true);
            Destroy(_enemySpawner.gameObject);
        }
    }


    void RefreshUIHealth(){
        var sizeDelta = foregroundCurrentHealth.sizeDelta;
        sizeDelta.x = (float) currentHealth / maxHealth * backgroundMaxHealth.sizeDelta.x;
        foregroundCurrentHealth.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);
    }
}