using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour{
    [SerializeField] private GameObject issuedItemPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float timeToIssueItem;
    private float time;

    private void Update(){
        time += Time.deltaTime;
        if (time >= timeToIssueItem){
            Instantiate(issuedItemPrefab, spawnTransform.position, Quaternion.identity, transform);
            time = 0;
        }
    }

    public void Die(){
        Destroy(gameObject);
    }
}