using System.IO;
using UnityEngine;
namespace MoheyBasicPack.JSON
{
    public class JSONReader : MonoBehaviour
    {
        [Tooltip("-Save data from inspector to file\n-Load data from file\n-Do Nothing")]
        [SerializeField] private ActionOnAwake actionOnAwake = ActionOnAwake.Load;

        private const string jsonFilepath = "/../";
        private const string jsonFileExtension = ".json";
        private string JSONFileFullPath;

        // Reference to the ScriptableObject
        [Tooltip("This data value is exposed to help instantiate and save JSON file for first time")]
        [SerializeField] private ScriptableObject startingData;
        ScriptableObject data;

        // Initialization
        private void Awake()
        {
            SetFilePath(startingData.name);
            UserDefaultAction();
        }

        private void SetFilePath(string fileName)
        {
            JSONFileFullPath = Application.dataPath + jsonFilepath + fileName + jsonFileExtension;
        }

        private void UserDefaultAction()
        {
            switch (actionOnAwake)
            {
                case ActionOnAwake.Save:
                    SaveData(startingData);
                    break;
                case ActionOnAwake.Load:
                    LoadData();
                    break;
                default:
                    break;
            }
        }

        // Public Save/Load
        
        public void SaveData(ScriptableObject newData)
        {
            if (newData != null)
            {
                string jsonData = JsonUtility.ToJson(newData);
                StreamWriter writer = new StreamWriter(JSONFileFullPath);
                writer.Write(jsonData);
                writer.Close();
            }
            else
            {
                Debug.LogWarning("Data ScriptableObject is not set.");
            }
        }

        public ScriptableObject GetData()
        {
            if (data == null)
                LoadData();
            return data;
        }

        private void LoadData()
        {
            if (startingData != null)
            {
                StreamReader reader = new StreamReader(JSONFileFullPath);
                string jsonData = reader.ReadToEnd();
                JsonUtility.FromJsonOverwrite(jsonData, startingData);
                reader.Close();
                data = startingData;
            }
            else
            {
                Debug.LogWarning("Data ScriptableObject is not set.");
            }
        }
    }
}