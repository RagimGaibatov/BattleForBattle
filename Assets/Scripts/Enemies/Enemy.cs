using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public enum EnemyState{
        Idling,
        SearchingTarget,
        Attacking
    }

    public EnemySpawner OriginSpawner{ private get; set; }

    public void Die(){
        OriginSpawner.ReclaimEnemy(this);
    }
}