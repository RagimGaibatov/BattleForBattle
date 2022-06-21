using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private Player _player;

    [SerializeField] private RectTransform backgroundMaxHealth;
    [SerializeField] private RectTransform foregroundCurrentHealth;

    private void Start(){
        _player = GetComponent<Player>();
        RefreshUIHealth();
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        _player.AnimationOfTakeDamage();
        if (currentHealth <= 0){
            _player.Die();
        }

        RefreshUIHealth();
    }

    void RefreshUIHealth(){
        var sizeDelta = foregroundCurrentHealth.sizeDelta;
        sizeDelta.x = (float) currentHealth / maxHealth * backgroundMaxHealth.sizeDelta.x;
        foregroundCurrentHealth.sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y);
    }
}