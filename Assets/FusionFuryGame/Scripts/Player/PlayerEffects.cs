using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace FusionFuryGame
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class PlayerEffects : MonoBehaviour
    {
        private CinemachineImpulseSource cinemachineImpulse;


        private void OnEnable()
        {
            PlayerShoot.onFire += ApplyShootFireEffect;
        }

        private void OnDisable()
        {
            PlayerShoot.onFire -= ApplyShootFireEffect;
        }


        private void Start()
        {
            cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        }


        private void ApplyShootFireEffect()
        {
            cinemachineImpulse.GenerateImpulse();
        }
    }
}
