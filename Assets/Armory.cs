using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour{
    [SerializeField] private Gun[] guns;
    private int currentIndexOfGun;

    public void ShotFromGun(){
        guns[currentIndexOfGun].Shot();
    }

    private void Update(){
        CheckInputForChangeGuns();
    }

    void CheckInputForChangeGuns(){
        int newIndex = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            newIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)){
            newIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            newIndex = 2;
        }

        if (newIndex != -1){
            ChangeGun(newIndex);
            currentIndexOfGun = newIndex;
        }
    }

    void ChangeGun(int indexOfNewGun){
        for (int i = 0; i < guns.Length; i++){
            guns[i].gameObject.SetActive(false);
        }
        guns[indexOfNewGun].gameObject.SetActive(true);
    }
}