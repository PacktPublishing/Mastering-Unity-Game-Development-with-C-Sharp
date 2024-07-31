using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FusionFuryGame
{
    public class HealthBar : MonoBehaviour
    {
        public Image healthImage;
        private Camera cameraMain;

        private void Start()
        {
            cameraMain = Camera.main;
        }
        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - cameraMain.transform.position);
        }
    }
}