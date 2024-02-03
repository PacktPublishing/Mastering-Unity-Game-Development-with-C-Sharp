using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeyAndIVGenerator : MonoBehaviour
{
    public static void GenerateKeyAndIV()
    {
        using (Aes aes = Aes.Create())
        {
            aes.GenerateKey();
            aes.GenerateIV();

            // Convert key and IV to base64 strings for easy storage and usage
            string base64Key = Convert.ToBase64String(aes.Key);
            string base64IV = Convert.ToBase64String(aes.IV);

            Debug.Log("Generated Key: " + base64Key);
            Debug.Log("Generated IV: " + base64IV);
        }
    }

    private void Start()
    {
        GenerateKeyAndIV();
    }
}
