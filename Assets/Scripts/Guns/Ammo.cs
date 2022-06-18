using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ammo : MonoBehaviour{
    [SerializeField] private int _amountOfAmmunition;
    [SerializeField] private Armory.GunType _gunType;


    private void OnTriggerEnter(Collider other){
        if (other.GetComponent<Player>()){
            Armory armory = other.GetComponentInChildren<Armory>();
            armory.AddAmmoToGun(_gunType, _amountOfAmmunition);
            Destroy(gameObject);
        }
    }
}