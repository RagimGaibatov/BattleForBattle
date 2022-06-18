using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField] private float speed = 5f;
    public Armory Armory;
    [SerializeField] private Animator animatorPlayer;

    [SerializeField] private Transform body;


    private void Update(){
        Move();
        Attack();
    }

    void Move(){
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputForward = Input.GetAxis("Vertical");

        Vector3 vectorMove = new Vector3(inputHorizontal, 0f, inputForward).normalized;

        transform.Translate(vectorMove * Time.deltaTime * speed);

        if (vectorMove != Vector3.zero){
            body.transform.rotation = Quaternion.LookRotation(vectorMove);
        }


        if (inputForward != 0 || inputHorizontal != 0){
            animatorPlayer.SetTrigger("Run");
        }
        else{
            animatorPlayer.SetTrigger("Idle");
        }
    }

    void Attack(){
        if (Input.GetKey(KeyCode.Space)){
            Armory.ShotFromGun();
        }
    }
}