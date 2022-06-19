using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public Health Health;
    public EnemySpawner OriginSpawner{ private get; set; }

    public void Die(){
        OriginSpawner.Reclaim(this);
    }
}