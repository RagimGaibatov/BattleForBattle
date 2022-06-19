using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ammo : MonoBehaviour{
    private enum GunType{
        Pistol,
        ShotGun,
        AKM
    }

    [SerializeField] private int _amountOfAmmunition;
    [SerializeField] private GunType _gunType;


    private void OnTriggerEnter(Collider other){
        if (other.GetComponent<Player>()){
            Armory armory = other.GetComponentInChildren<Armory>();
            armory.AddAmmoToGun(Type.GetType(_gunType.ToString()), _amountOfAmmunition);
            Destroy(gameObject);
        }
    }
}