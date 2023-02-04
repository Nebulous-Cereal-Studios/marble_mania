using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataContainer : MonoBehaviour
{
    public static PlayerDataContainer Instance;

    private void Awake() {
        if(Instance != null) {
            Destroy(this);
        }

        Instance = this;
        data = DataStorage.Load();
        DontDestroyOnLoad(this);
    }
    public DataStorage data;

    [System.Serializable]
    public class DataStorage {
        static string saveDataPath {
            get {
                return Path.Combine(Application.persistentDataPath, "savedata.json");
            }
        }
        public string[] cosmetics = new string[3];
        public int deaths = 0;

        public void Save() {
            File.WriteAllText(saveDataPath, JsonUtility.ToJson(this, true));
        }

        public static DataStorage Load() {
            if(!File.Exists(saveDataPath)) {
                var ds = new DataStorage();
                ds.Save();
                return ds;
            }
            return JsonUtility.FromJson<DataStorage>(File.ReadAllText(saveDataPath));
        } 
    }

    private void OnApplicationQuit() {
        data.Save();
    }
}
