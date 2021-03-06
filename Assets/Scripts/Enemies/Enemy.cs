using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, ISaveable{
    public enum EnemyState{
        Attacking,
        Walking,
        Dying
    }

    [SerializeField] int goldForMurder;

    [SerializeField] private float movementSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float maxDistanceToAttack;

    [field: SerializeField] public int SaveId{ get; private set; }

    public EnemySpawner OriginSpawner{ private get; set; }

    private AllPlants _allPlants;


    private TargetForEnemy _targetForEnemy;
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

        if (_targetForEnemy && targetVector.magnitude < maxDistanceToAttack){
            _enemyState = EnemyState.Attacking;
        }
        else{
            _enemyState = EnemyState.Walking;
        }

        if (_enemyState == EnemyState.Walking){
            _animatorEnemy.SetTrigger("Walk");
            if (_targetForEnemy == null){
                float randomValue = Random.value;
                if (randomValue <= 0.9f){
                    Vector3 shortestVector = Vector3.positiveInfinity;
                    for (int i = 0; i < _allPlants.plantsList.Count; i++){
                        Vector3 vectorBetweenTargets = _allPlants.plantsList[i].transform.position - transform.position;
                        if (shortestVector.sqrMagnitude > vectorBetweenTargets.sqrMagnitude){
                            shortestVector = vectorBetweenTargets;
                            _targetForEnemy = _allPlants.plantsList[i];
                        }
                    }
                }
                else{
                    _targetForEnemy = _playerHealth;
                }
            }
            else{
                targetVector = _targetForEnemy.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(targetVector);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
        }

        if (_enemyState == EnemyState.Attacking){
            targetVector = _targetForEnemy.transform.position - transform.position;

            _animatorEnemy.SetTrigger("Attack");
        }
    }

    public void DealDamage(){
        _targetForEnemy.TakeDamage(damage);
    }


    public void Die(){
        _enemyState = EnemyState.Dying;
        _animatorEnemy.SetTrigger("Death");
        Resources resources = FindObjectOfType<Resources>();
        resources.Gold += goldForMurder;
        resources.UpdateGoldText();
        StartCoroutine(OriginSpawner.ReclaimEnemy(this));
    }

    public SaveData Save(){
        SaveData _saveData = new SaveData();
        _saveData.saveId = SaveId;
        _saveData.loadType = LoadType.New;
        _saveData.position = transform.position;
        _saveData.health = GetComponent<EnemyHealth>().Health;
        return _saveData;
    }

    public void Load(SaveData data){
        GetComponent<EnemyHealth>().Health = data.health;
        transform.position = data.position;
        FindObjectOfType<EnemySpawner>().AddEnemy(this);
    }
}