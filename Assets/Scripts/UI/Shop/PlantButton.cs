using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlantButton : MonoBehaviour{
    [SerializeField] private Plant _plantPrefab;
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private LayerMask groundLayerMask;


    private PlayerGold _playerGold;
    public Plant _newPlant;


    private void Start(){
        _playerGold = FindObjectOfType<PlayerGold>();
        priceText.text = price.ToString();
    }

    public void TryBuyPlant(){
        if (_playerGold.Gold >= price){
            _newPlant = Instantiate(_plantPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Update(){
        if (_newPlant == null){
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool isHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask);
        if (isHit)
            _newPlant.transform.position = new Vector3(hit.point.x, _plantPrefab.transform.position.y, hit.point.z);
        if (Input.GetMouseButtonDown(0)){
            if (isHit){
                if (EventSystem.current.IsPointerOverGameObject() == false){
                    _playerGold.Gold -= price;
                    _playerGold.UpdateGoldText();
                    _newPlant = null;
                }
            }
            else{
                Destroy(_newPlant.gameObject);
            }
        }
    }
}