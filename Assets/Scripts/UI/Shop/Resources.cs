using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour, ISaveable{
    [SerializeField] private int gold;
    [SerializeField] private TextMeshProUGUI goldText;
    [field: SerializeField] public int SaveId{ get; private set; }

    public int Gold{
        get => gold;
        set => gold = value;
    }

    private void Start(){
        goldText.text = "Gold : " + gold;
    }

    public void UpdateGoldText(){
        goldText.text = "Gold : " + gold;
    }

    public SaveData Save(){
        SaveData _saveData = new SaveData();
        _saveData.saveId = SaveId;
        _saveData.loadType = LoadType.Restore;
        _saveData.playerGold = gold;
        return _saveData;
    }

    public void Load(SaveData data){
        gold = data.playerGold;
        UpdateGoldText();
    }
}