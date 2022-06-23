using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour{
    public enum EnemyState{
        Attacking,
        Walking,
        Dying
    }

    [SerializeField] int goldForMurder;

    [SerializeField] private float movementSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float maxDistanceToAttack;
    [SerializeField] private float periodAttack;
    private float lastAttackTime;
    public EnemySpawner OriginSpawner{ private get; set; }

    private AllPlants _allPlants;
    private Plant _plantTarget;
    private PlayerHealth _playerHealth;

    private Animator _animatorEnemy;
    private EnemyState _enemyState;

    private Vector3 targetVector = Vector3.positiveInfinity;

    private void Start(){
        _allPlants = FindObjectOfType<AllPlants>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _animatorEnemy = GetComponent<Animator>();
    }

    private void Update(){
        if (_enemyState == EnemyState.Dying){
            return;
        }

        if (_plantTarget && targetVector.magnitude < maxDistanceToAttack){
            _enemyState = EnemyState.Attacking;
        }
        else{
            _enemyState = EnemyState.Walking;
        }

        if (_enemyState == EnemyState.Walking){
            _animatorEnemy.SetTrigger("Walk");
            if (_plantTarget == null){
                Vector3 shortestVector = Vector3.positiveInfinity;
                for (int i = 0; i < _allPlants.plantsList.Count; i++){
                    Vector3 vectorBetweenTargets = _allPlants.plantsList[i].transform.position - transform.position;
                    if (shortestVector.sqrMagnitude > vectorBetweenTargets.sqrMagnitude){
                        shortestVector = vectorBetweenTargets;
                        _plantTarget = _allPlants.plantsList[i];
                        targetVector = _plantTarget.transform.position - transform.position;
                    }
                }
            }
            else{
                targetVector = _plantTarget.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(targetVector);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
        }

        if (_enemyState == EnemyState.Attacking){
            if (Time.time > lastAttackTime + periodAttack){
                _animatorEnemy.SetTrigger("Attack");
                lastAttackTime = Time.time;
                _plantTarget._plantHealth.TakeDamage(damage);
            }
        }
    }


    public void Die(){
        _enemyState = EnemyState.Dying;
        _animatorEnemy.SetTrigger("Death");
        Resources resources = FindObjectOfType<Resources>();
        resources.Gold += goldForMurder;
        resources.UpdateGoldText();
        StartCoroutine(OriginSpawner.ReclaimEnemy(this));
    }
}