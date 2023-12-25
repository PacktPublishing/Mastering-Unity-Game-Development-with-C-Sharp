using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace FusionFuryGame
{
    public class CameraManager : Singlton<CameraManager>
    {
        // Dictionary to map enum values to Cinemachine virtual cameras
        public GenericDictionary<CameraType, CinemachineVirtualCamera> cameraDictionary = new GenericDictionary<CameraType, CinemachineVirtualCamera>();

        // Reference to the currently active virtual camera
        private CinemachineVirtualCamera currentCamera;

        void Start()
        {
            SwitchCamera(CameraType.PlayerCamera);
        }


        // Function to switch between virtual cameras using the enum
        public void SwitchCamera(CameraType newCameraType)
        {
            // Disable the current camera
            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(false);
            }

            // Enable the new camera based on the enum
            if (cameraDictionary.ContainsKey(newCameraType))
            {
                currentCamera = cameraDictionary[newCameraType];
                currentCamera.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Camera of type " + newCameraType + " not found in the dictionary.");
            }
        }
    }

    // Enum to represent different cameras
    public enum CameraType
    {
        PlayerCamera,
        BossCamera,
        // Add more camera types as needed
    }
}