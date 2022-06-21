using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour{
    [SerializeField] private int amountOfHealth;

    private void OnTriggerEnter(Collider other){
        if (other.TryGetComponent(out PlayerHealth playerHealth)){
            if (playerHealth.CurrentHealth < playerHealth.MAXHealth){
                playerHealth.AddHealth(amountOfHealth);
                Destroy(gameObject);
            }
        }
    }
}