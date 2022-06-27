using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonSaveSystem : MonoBehaviour{
    [SerializeField] private MonoBehaviour[] saveablesMonoBehaviours;

    private string _filePath;

    private IEnumerable<ISaveable> saveables;

    public static JsonSaveSystem instance;

    private void Awake(){
        saveables = saveablesMonoBehaviours.OfType<ISaveable>();
        _filePath = Application.persistentDataPath + "/Save.json";
    }

    void Start(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.P)){
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L)){
            StartCoroutine(LoadGame());
        }
    }

    void SaveGame(){
        IEnumerable<ISaveable> saveablesOnScene = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        using (var writer = new StreamWriter(_filePath)){
            foreach (var saveableObject in saveablesOnScene){
                var json = JsonUtility.ToJson(saveableObject.Save());
                writer.WriteLine(json);
            }
        }
    }

    IEnumerator LoadGame(){
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        List<ISaveable> saveablesOnScene = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();
       
        using (var reader = new StreamReader(_filePath)){
            string line;
            while ((line = reader.ReadLine()) != null){
                SaveData saveData = JsonUtility.FromJson<SaveData>(line);
                if (saveData.loadType == LoadType.New){
                    ISaveable saveable = saveables.First(e => e.SaveId == saveData.saveId);
                    MonoBehaviour newObject = Instantiate((MonoBehaviour) saveable);
                    ((ISaveable) newObject).Load(saveData);
                }
                else{
                    ISaveable saveable = saveablesOnScene.FirstOrDefault(e => e.SaveId == saveData.saveId);
                    saveablesOnScene.Remove(saveable);
                    saveable?.Load(saveData);
                }
            }
        }
    }
}