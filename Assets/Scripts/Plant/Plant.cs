using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class Plant : MonoBehaviour{
    [SerializeField] private GameObject issuedItemPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float timeToIssueItem;
    public PlantHealth _plantHealth;

    private AllPlants _allPlants;
    private float time;

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

    public void Die(){
        _allPlants.RemovePlantFromList(this);
        Destroy(gameObject);
    }
}