using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class PlayerCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BossArea"))
            {
                CameraManager.Instance.SwitchCamera(CameraType.BossCamera);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BossArea"))
            {
                CameraManager.Instance.SwitchCamera(CameraType.PlayerCamera);
            }
        }
    }
}