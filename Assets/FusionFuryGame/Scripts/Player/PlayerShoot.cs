using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace FusionFuryGame {
    public class PlayerShoot : MonoBehaviour
    {
        public static UnityAction onFire = delegate { };
        private void OnEnable()
        {
            PlayerInput.onShoot += OnShootFire;
        }

        private void OnDisable()
        {
            PlayerInput.onShoot -= OnShootFire;
        }


        private void OnShootFire()
        {
            onFire.Invoke();
        }
    }
}