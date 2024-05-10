using UnityEngine;

namespace Chapter6
{
    public class SettingsManager : MonoBehaviour
    {
        public GameSettings gameSettings;

        // Serialize the GameSettings ScriptableObject to a file 
        public void SaveSettings()
        {
            string jsonSettings = JsonUtility.ToJson(gameSettings);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/settings.json", jsonSettings);
        }

        // Deserialize the GameSettings ScriptableObject from a file 
        public void LoadSettings()
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/settings.json"))
            {
                string jsonSettings = System.IO.File.ReadAllText(Application.persistentDataPath + "/settings.json");
                gameSettings = JsonUtility.FromJson<GameSettings>(jsonSettings);
            }
        }

    }

}