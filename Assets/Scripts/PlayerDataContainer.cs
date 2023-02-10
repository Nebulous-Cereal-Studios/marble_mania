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
        public Vector3 verticalDirection = new Vector3(0, 0, 1);
        public Vector3 horizontalDirection  = new Vector3(1, 0);

        public void Save() {
            File.WriteAllText(saveDataPath, JsonUtility.ToJson(this, true));
        }

        public static DataStorage Load() {
            if(!File.Exists(saveDataPath)) {
                var ds = new DataStorage();
                ds.Save();
                return ds;
            }
            return Validate(JsonUtility.FromJson<DataStorage>(File.ReadAllText(saveDataPath)));
        } 

        public static DataStorage Validate(DataStorage ds) {
            if(ds.cosmetics.Length > 3) {
                ds.cosmetics = new string[3];
            }
            if(ds.deaths < 0) {
                ds.deaths = 0;
            }
            if(Mathf.Abs(ds.verticalDirection.x) + Mathf.Abs(ds.verticalDirection.z) != 1 || ds.verticalDirection.y != 0) {
                ds.verticalDirection = new Vector3(0, 0, 1);
            }
            if(Mathf.Abs(ds.horizontalDirection.x) + Mathf.Abs(ds.horizontalDirection.z) != 1 || ds.horizontalDirection.y != 0) {
                ds.horizontalDirection = new Vector3(1, 0);
            }

            return ds;
        }
    }

    private void OnApplicationQuit() {
        data.Save();
    }
}
