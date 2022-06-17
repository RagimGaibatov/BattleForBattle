using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Armory _armory;
    [SerializeField] private Animator animatorPlayer;


    private void Update(){
        Move();
        Attack();
    }

    void Move(){
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputForward = Input.GetAxis("Vertical");

        transform.Translate(inputHorizontal * Time.deltaTime * speed, 0f, inputForward * Time.deltaTime * speed);
        if (inputForward != 0 || inputHorizontal != 0){
            animatorPlayer.SetTrigger("Run");
        }
        else{
            animatorPlayer.SetTrigger("Idle");
        }
    }

    void Attack(){
        if (Input.GetKey(KeyCode.Space)){
            _armory.ShotFromGun();
        }
    }
}