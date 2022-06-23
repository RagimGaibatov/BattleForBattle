using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Armory Armory;

    private Animator _animatorPlayer;
    private CharacterController _characterController;

    private bool isAlive = true;


    private void Awake(){
        _animatorPlayer = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update(){
        if (!isAlive){
            _animatorPlayer.SetTrigger("Death");
            return;
        }

        Move();
        Attack();
    }

    void Move(){
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputForward = Input.GetAxis("Vertical");

        Vector3 vectorMove = new Vector3(inputHorizontal, 0f, inputForward).normalized;

        _characterController.Move(vectorMove * Time.deltaTime * speed);
        if (vectorMove != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(vectorMove);
        }


        if (vectorMove != new Vector3(0, 0, 0)){
            _animatorPlayer.SetTrigger("Run");
        }
        else{
            _animatorPlayer.SetTrigger("Idle");
        }
    }

    void Attack(){
        if (Input.GetKey(KeyCode.Space)){
            Armory.ShotFromGun();
        }
    }

    public void Die(){
        if (isAlive){
            _animatorPlayer.SetTrigger("Death");
        }

        isAlive = false;
    }

    public void AnimationOfTakeDamage(){
        _animatorPlayer.SetTrigger("TakeDamage");
    }
}