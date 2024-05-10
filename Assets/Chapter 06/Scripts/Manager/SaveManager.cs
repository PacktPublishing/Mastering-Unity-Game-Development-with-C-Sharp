using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace Chapter6
{
    public class SaveManager : MonoBehaviour
    {
        private const string saveFileName = "saveData1.dat";
        private const string cloudSaveFileName = "cloudSaveData.dat";
        private static byte[] key = Convert.FromBase64String("kwAXmhR48HenPp04YXrKSNfRcFSiaQx35BlHnI7kzK0=");
        private static byte[] iv = Convert.FromBase64String("GcVb7iqWex9uza+Fcb3BCQ==");

        public static void SaveData(string key, string data)
        {
           string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

            // Load existing data 
            Dictionary<string, string> savedData = LoadSavedData();

            // Add or update data based on its key 
            savedData[key] = data;

            // Serialize the entire dictionary 
            string jsonData = JsonConvert.SerializeObject(savedData);
            byte[] encryptedData = EncryptData(jsonData);

            // Write the serialized data to the file 
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.Write(encryptedData, 0, encryptedData.Length);
            }

        }

        public static string LoadData(string key)
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

            // Load existing data 
            Dictionary<string, string> savedData = LoadSavedData();

            // Extract data based on its key 
            if (savedData.ContainsKey(key))
            {
                return savedData[key];
            }
            else
            {
                Debug.LogWarning("No save data found for key: " + key);
                return null;
            }
        }

        private static Dictionary<string, string> LoadSavedData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

            if (File.Exists(filePath))
            {
                byte[] encryptedData = File.ReadAllBytes(filePath);
                string jsonData = DecryptData(encryptedData);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            }
            else
            {
                Debug.LogWarning("No save data found.");
                return new Dictionary<string, string>();
            }
        }

        public static void DeleteSaveData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("Save data deleted.");
            }
            else
            {
                Debug.LogWarning("No save data found to delete.");
            }
        }
        private static byte[] EncryptData(string data)
        {
           using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(data);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        private static string DecryptData(byte[] encryptedData)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                   using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

}