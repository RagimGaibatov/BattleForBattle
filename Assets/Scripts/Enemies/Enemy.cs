using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public enum EnemyState{
        SearchingTarget,
        Attacking,
        Walking,
        Dying
    }

    [SerializeField] int goldForMurder;
    public EnemySpawner OriginSpawner{ private get; set; }


    public void Die(){
        OriginSpawner.ReclaimEnemy(this);
        Resources resources = FindObjectOfType<Resources>();
        resources.Gold += goldForMurder;
        resources.UpdateGoldText();
    }
}