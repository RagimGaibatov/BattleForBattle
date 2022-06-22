using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlantButton : MonoBehaviour{
    [SerializeField] private Plant _plantPrefab;
    [SerializeField] private GhostPlant _ghostPlantPrefab;
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask plantLayerMask;

    [SerializeField] private Vector3 sizeHalfBox;
    private GhostPlant _GhostPlant;
    private bool isActive = false;

    private Resources _playerResources;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip errorBuildingClip;
    [SerializeField] private AudioClip buildedClip;
    [SerializeField] private AudioClip notEnoughGoldClip;

    private void Start(){
        _playerResources = FindObjectOfType<Resources>();
        priceText.text = price.ToString();
        _GhostPlant = Instantiate(_ghostPlantPrefab, transform.position, Quaternion.identity);
        _GhostPlant.gameObject.SetActive(false);
    }

    public void TryPlacingPlant(){
        if (_playerResources.Gold >= price){
            GhostOn();
        }
        else{
            _audioSource.PlayOneShot(notEnoughGoldClip);
        }
    }

    private void Update(){
        if (isActive == false){
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            GhostOff();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isCursorOverGround = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask);

        bool isTouching = Physics.CheckBox(hit.point, sizeHalfBox, Quaternion.identity, plantLayerMask);
        if (isTouching || isCursorOverGround == false){
            _GhostPlant.SetRedColor();
        }
        else{
            _GhostPlant.SetGreenColor();
        }

        if (isCursorOverGround){
            _GhostPlant.transform.position = new Vector3(hit.point.x, _plantPrefab.transform.position.y, hit.point.z);
        }

        if (Input.GetMouseButtonDown(0)){
            if (EventSystem.current.IsPointerOverGameObject() == false){
                if (isTouching == false && isCursorOverGround){
                    _playerResources.Gold -= price;
                    _playerResources.UpdateGoldText();
                    Instantiate(_plantPrefab, _GhostPlant.transform.position, Quaternion.identity);
                    GhostOff();
                    _audioSource.PlayOneShot(buildedClip);
                }

                _audioSource.PlayOneShot(errorBuildingClip);
            }
        }
    }

    void GhostOn(){
        _GhostPlant.gameObject.SetActive(true);
        isActive = true;
    }

    void GhostOff(){
        _GhostPlant.gameObject.SetActive(false);
        isActive = false;
    }
}