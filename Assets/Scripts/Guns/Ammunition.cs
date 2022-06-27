using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ammunition : MonoBehaviour, ISaveable{
    AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private enum GunType{
        Pistol,
        ShotGun,
        AKM
    }

    [SerializeField] private int _amountOfAmmunition;
    [SerializeField] private GunType _gunType;

    [field: SerializeField] public int SaveId{ get; private set; }


    private void OnTriggerEnter(Collider other){
        if (other.GetComponent<Player>()){
            Armory armory = other.GetComponentInChildren<Armory>();
            armory.AddAmmoToGun(Type.GetType(_gunType.ToString()), _amountOfAmmunition);
            _audioSource = FindObjectOfType<AudioSource>();
            _audioSource.PlayOneShot(_audioClip);
            Destroy(gameObject);
        }
    }

    public SaveData Save(){
        SaveData saveData = new SaveData();
        saveData.saveId = SaveId;
        saveData.loadType = LoadType.New;
        saveData.position = transform.position;
        return saveData;
    }

    public void Load(SaveData data){
        transform.position = data.position;
    }
}