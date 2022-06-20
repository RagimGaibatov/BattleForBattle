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


    public void Start(){
        UpdateAmmoText();
        _gun.OnUpdateAmmo += UpdateAmmoText;
    }


    void UpdateAmmoText(){
        ammoText.text = _gun.Ammo.ToString();
    }
}