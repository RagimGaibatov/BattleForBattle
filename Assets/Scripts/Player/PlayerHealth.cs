using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITargetForEnemy{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private Player _player;

    [SerializeField] private RectTransform backgroundMaxHealth;
    [SerializeField] private RectTransform foregroundCurrentHealth;

    public int CurrentHealth => currentHealth;
    public int MAXHealth => maxHealth;

    private void Start(){
        _player = GetComponent<Player>();
        RefreshUIHealth();
    }

    public void TakeDamageOnYourself(int damage){
        TakeDamage(damage);
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        _player.AnimationOfTakeDamage();
        if (currentHealth <= 0){
            _player.Die();
        }

        RefreshUIHealth();
    }


    public void AddHealth(int health){
        currentHealth += health;
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

        RefreshUIHealth();
    }


    void RefreshUIHealth(){
        var sizeDelta = foregroundCurrentHealth.sizeDelta;
        sizeDelta.x = (float) currentHealth / maxHealth * backgroundMaxHealth.sizeDelta.x;
        foregroundCurrentHealth.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);
    }
}