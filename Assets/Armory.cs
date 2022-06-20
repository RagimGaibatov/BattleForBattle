using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour{
    [SerializeField] private Gun[] guns;
    private int currentIndexOfGun;


    private void Awake(){
        for (int i = 0; i < guns.Length; i++){
            if (guns[i].gameObject.activeInHierarchy){
                currentIndexOfGun = i;
                ChangeGun(currentIndexOfGun);
                return;
            }
        }
    }

    private void Update(){
        ChangeGunByInput();
    }

    public void AddAmmoToGun(Type gunType, int amountOfAmmunition){
        for (int i = 0; i < guns.Length; i++){
            if (guns[i].GetType() == gunType){
                guns[i].AddAmmo(amountOfAmmunition);
            }
        }
    }


    public void ShotFromGun(){
        guns[currentIndexOfGun].Shot();
    }


    private void ChangeGunByInput(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            currentIndexOfGun = 0;
            ChangeGun(currentIndexOfGun);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            currentIndexOfGun = 1;
            ChangeGun(currentIndexOfGun);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            currentIndexOfGun = 2;
            ChangeGun(currentIndexOfGun);
        }
    }

    void ChangeGun(int indexOfNewGun){
        for (int i = 0; i < guns.Length; i++){
            guns[i].gameObject.SetActive(false);
        }

        guns[indexOfNewGun].gameObject.SetActive(true);
    }
}