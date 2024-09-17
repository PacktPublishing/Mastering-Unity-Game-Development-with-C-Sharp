
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FusionFuryGame
{
    public class PlayerAudio : MonoBehaviour
    {
        public AudioClip shootSound;
        public AudioClip hitSound;
        public AudioClip abilitySound;

        private void OnEnable()
        {
            PlayerInput.onShoot += PlayShootSound;
            HUDView.onAbilityPressed += PlayAbilitySound;
            PlayerCollision.onPlayerGetHit += PlayHitSound;
        }

        private void OnDisable()
        {
            PlayerInput.onShoot -= PlayShootSound;
            HUDView.onAbilityPressed -= PlayAbilitySound;
            PlayerCollision.onPlayerGetHit -= PlayHitSound;
        }


        private void PlayShootSound()
        {
            AudioManager.Instance.PlaySFX(shootSound, 2);
        }

        private void PlayHitSound()
        {
            AudioManager.Instance.PlaySFX(hitSound, 8);

        }

        private void PlayAbilitySound()
        {
            AudioManager.Instance.PlaySFX(abilitySound , 2);
        }
    }
}