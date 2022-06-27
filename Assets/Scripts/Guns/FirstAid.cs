using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour, ISaveable{
    [SerializeField] private int amountOfHealth;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;


    [field: SerializeField] public int SaveId{ get; private set; }

    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out PlayerHealth playerHealth)){
            if (playerHealth.CurrentHealth < playerHealth.MAXHealth){
                playerHealth.AddHealth(amountOfHealth);
                _audioSource = FindObjectOfType<AudioSource>();
                _audioSource.PlayOneShot(_audioClip);
                Destroy(gameObject);
            }
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