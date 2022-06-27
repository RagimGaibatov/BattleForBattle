using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : TargetForEnemy, ISaveable{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private RectTransform backgroundMaxHealth;
    [SerializeField] private RectTransform foregroundCurrentHealth;
    private Player _player;

    [field: SerializeField] public int SaveId{ get; private set; }
    public int CurrentHealth => currentHealth;
    public int MAXHealth => maxHealth;

    private void Start(){
        _player = GetComponent<Player>();
        RefreshUIHealth();
    }

    public override void TakeDamage(int damage){
        if (currentHealth <= 0){
            return;
        }

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

    public SaveData Save(){
        SaveData saveData = new SaveData();
        saveData.saveId = SaveId;
        saveData.loadType = LoadType.Restore;
        saveData.health = currentHealth;
        saveData.position = transform.position;
        return saveData;
    }

    public void Load(SaveData data){
        currentHealth = data.health;
        GetComponent<CharacterController>().enabled = false;
        transform.position = data.position;
        GetComponent<CharacterController>().enabled = true;
    }
}