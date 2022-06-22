using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour{
    [SerializeField] private int amountOfHealth;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;


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
}