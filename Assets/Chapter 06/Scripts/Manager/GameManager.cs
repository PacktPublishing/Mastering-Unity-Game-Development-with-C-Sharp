using UnityEngine;

namespace Chapter6
{
    public class GameManager : MonoBehaviour
    {
        public PlayerData playerData;
        public GameSettings gameSettings;

        private void Start()
        {
            LoadGameData();
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }
        private void LoadGameData()
        {
            if (playerData == null)
            {
            }
            else
            {
                JsonUtility.FromJsonOverwrite(SaveManager.LoadData("playerData"), playerData);
            }

            if (gameSettings == null)
            {
                gameSettings = ScriptableObject.CreateInstance<GameSettings>();
            }
            else
            {
                JsonUtility.FromJsonOverwrite(SaveManager.LoadData("gameSettings"), gameSettings);
            }
        }

        private void SaveGameData()
        {
            SaveManager.SaveData("playerData", JsonUtility.ToJson(playerData));
            SaveManager.SaveData("gameSettings", JsonUtility.ToJson(gameSettings));
        }
    }

}