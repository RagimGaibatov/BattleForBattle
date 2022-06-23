using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlants : MonoBehaviour{
    public List<Plant> plantsList;

    private void Awake(){
        plantsList = new List<Plant>();
    }

    public void AddPlantToList(Plant plant){
        plantsList.Add(plant);
    }

    public void RemovePlantFromList(Plant plant){
        plantsList.Remove(plant);
    }
}