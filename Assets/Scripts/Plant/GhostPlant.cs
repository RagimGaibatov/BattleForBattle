using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GhostPlant : MonoBehaviour{
    private Material _material;


    private void Awake(){
        _material = GetComponentInChildren<Renderer>().material;
    }


    public void SetRedColor(){
        _material.color = Color.red;
    }

    public void SetGreenColor(){
        _material.color = Color.green;
    }
}