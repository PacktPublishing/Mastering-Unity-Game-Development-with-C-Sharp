using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

namespace FusionFuryGame
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class PlayerEffects : MonoBehaviour
    {
        private CinemachineImpulseSource cinemachineImpulse;
        [SerializeField] ParticleSystem specialShotEffect;
        public static UnityAction onParticleFinish = delegate { };
        

        private void OnEnable()
        {
            PlayerShoot.onFire += ApplyShootFireEffect;
            PlayerAbility.OnAbilityActivated += PlaySpecialShotEffect;
        }

        private void OnDisable()
        {
            PlayerShoot.onFire -= ApplyShootFireEffect;
            PlayerAbility.OnAbilityActivated -= PlaySpecialShotEffect;
        }


        private void Start()
        {
            cinemachineImpulse = GetComponent<CinemachineImpulseSource>();
        }


        private void ApplyShootFireEffect()
        {
            cinemachineImpulse.GenerateImpulse();
        }


        private void PlaySpecialShotEffect()
        {
            if (specialShotEffect != null)
            {
                specialShotEffect.Play();
                Invoke("ParticleFinished", specialShotEffect.duration - 1f);
            }
        }


        private void ParticleFinished()
        {
            Debug.Log("OnPlayparticle Effect");
            onParticleFinish.Invoke();
        }
    }
}
