using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviour{
    [SerializeField] private int gold;
    [SerializeField] private TextMeshProUGUI goldText;


    public int Gold{
        get => gold;
        set => gold = value;
    }

    private void Start(){
        goldText.text = "Gold : " + gold;
    }

    public void UpdateGoldText(){
        goldText.text = "Gold : " + gold;
    }
}