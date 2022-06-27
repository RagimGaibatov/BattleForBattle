using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Gun _gun;


    private void OnEnable(){
        _gun.OnUpdateAmmo += UpdateAmmoText;
    }

    private void OnDisable(){
        _gun.OnUpdateAmmo -= UpdateAmmoText;
    }

    public void Start(){
        UpdateAmmoText();
    }


    void UpdateAmmoText(){
        ammoText.text = _gun.Ammo.ToString();
    }
}