using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconsUI : MonoBehaviour{
    [SerializeField] private Image[] iconsGun;
    [SerializeField] private Armory _armory;
    private int currentIndexOfGun;


    public void Awake(){
        ChangeGunsIcons();
        _armory.OnChangeGun += ChangeGunsIcons;
    }

    void ChangeGunsIcons(){
        currentIndexOfGun = _armory.CurrentIndexOfGun;
        for (int i = 0; i < _armory.GunsLength; i++){
            Unselect(iconsGun[i]);
        }

        Select(iconsGun[currentIndexOfGun]);
    }

    void Select(Image image){
        image.color = new Color(1, 1, 1, 0.5f);
    }

    void Unselect(Image image){
        image.color = new Color(0, 0, 0, 0.5f);
    }
}