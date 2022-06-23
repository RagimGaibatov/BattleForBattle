using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Armory : MonoBehaviour{
    [SerializeField] private Gun[] guns;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    private int currentIndexOfGun;

    public int CurrentIndexOfGun => currentIndexOfGun;
    public int GunsLength => guns.Length;

    public event Action OnChangeGun;

    public void Start(){
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
        OnChangeGun?.Invoke();
        _audioSource.PlayOneShot(_clip);
    }
}