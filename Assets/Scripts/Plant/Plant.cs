using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Plant : TargetForEnemy, ISaveable{
    [SerializeField] private GameObject issuedItemPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float timeToIssueItem;
    public PlantHealth _plantHealth;

    private AllPlants _allPlants;
    private float time;

    [field: SerializeField] public int SaveId{ get; private set; }

    private void Start(){
        _allPlants = FindObjectOfType<AllPlants>();
    }

    private void Update(){
        time += Time.deltaTime;
        if (time >= timeToIssueItem){
            Instantiate(issuedItemPrefab, spawnTransform.position, Quaternion.identity, transform);
            time = 0;
        }
    }

    public override void TakeDamage(int damage){
        _plantHealth.TakeDamage(damage);
    }

    public void Die(){
        _allPlants.RemovePlantFromList(this);
        Destroy(gameObject);
    }

    public SaveData Save(){
        SaveData _saveData = new SaveData();
        _saveData.saveId = SaveId;
        _saveData.loadType = LoadType.New;
        _saveData.health = _plantHealth.Health;
        _saveData.position = transform.position;
        return _saveData;
    }

    public void Load(SaveData data){
        _plantHealth.Health = data.health;
        transform.position = data.position;
        _allPlants.AddPlantToList(this);
    }
}