using System;
using UnityEngine;

public enum LoadType{
    None = 0,
    New = 10,
    Restore = 20
}

[Serializable]
public class SaveData {
    public LoadType loadType;
    public int saveId;

    public Vector3 position;
    public int health;

    public int ammo;

    public float timeToSpawn;
    public int playerGold;
}