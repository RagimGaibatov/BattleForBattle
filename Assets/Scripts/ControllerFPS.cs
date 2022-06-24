using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFPS : MonoBehaviour{
    [SerializeField] private int maxFPS = 60;


    void Update(){
        Application.targetFrameRate = maxFPS;
    }
}