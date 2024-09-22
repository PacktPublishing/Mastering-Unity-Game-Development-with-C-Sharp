using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

namespace FusionFuryGame
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class PlayerEffects : MonoBehaviour
    {
        private CinemachineImpulseSource cinemachineImpulse;
        [SerializeField] ParticleSystem specialShotEffect;
        [SerializeField] ParticleSystem muzzleEffect;
        public static UnityAction onParticleFinish = delegate { };

        // Post-processing volume reference
        private Volume postProcessVolume;
        private ChromaticAberration chromaticAberration;
        private MotionBlur motionBlur;
        private LensDistortion lensDistortion;

        // Chromatic Aberration values for ability
        private float originalChromaticAberration;
        [SerializeField] private float abilityChromaticAberration = 0.5f; // Increased value during ability

        // Motion Blur and Lens Distortion values for dash
        private float originalMotionBlurIntensity;
        private float originalLensDistortionIntensity;
        [SerializeField] private float dashMotionBlurIntensity = 1f; // Increased during dash
        [SerializeField] private float dashLensDistortionIntensity = -0.3f; // Distortion during dash

        // Dash time reference (passed from PlayerMovement)
        private float dashTime;

        private void OnEnable()
        {
            PlayerShoot.onFire += ApplyShootFireEffect;
            PlayerAbility.OnAbilityActivated += PlaySpecialShotEffect;
            PlayerAbility.OnAbilityActivated += StartChromaticAberrationEffect;
            PlayerAbility.OnAbilityDeactivated += ResetChromaticAberrationEffect;
            PlayerMovement.OnDashStarted += HandleDashStarted;
            PlayerMovement.OnDashEnded += HandleDashEnded;
        }

        private void OnDisable()
        {
            PlayerShoot.onFire -= ApplyShootFireEffect;
            PlayerAbility.OnAbilityActivated -= PlaySpecialShotEffect;
            PlayerAbility.OnAbilityActivated -= StartChromaticAberrationEffect;
            PlayerAbility.OnAbilityDeactivated -= ResetChromaticAberrationEffect;
            PlayerMovement.OnDashStarted -= HandleDashStarted;
            PlayerMovement.OnDashEnded -= HandleDashEnded;
        }


        private void Start()
        {
            cinemachineImpulse = GetComponent<CinemachineImpulseSource>();

            // Get the Post Process Volume and settings
            postProcessVolume = FindObjectOfType<Volume>();

            // Initialize effects (Chromatic Aberration, Motion Blur, Lens Distortion)
            if (postProcessVolume.profile.TryGet(out ChromaticAberration chromaticAberrationComponent))
            {
                chromaticAberration = chromaticAberrationComponent;
                originalChromaticAberration = chromaticAberration.intensity.value;
            }
            if (postProcessVolume.profile.TryGet(out MotionBlur motionBlurComponent))
            {
                motionBlur = motionBlurComponent;
                originalMotionBlurIntensity = motionBlur.intensity.value;
            }
            if (postProcessVolume.profile.TryGet(out LensDistortion lensDistortionComponent))
            {
                lensDistortion = lensDistortionComponent;
                originalLensDistortionIntensity = lensDistortion.intensity.value;
            }
        }


        private void ApplyShootFireEffect()
        {
            cinemachineImpulse.GenerateImpulse();
            muzzleEffect?.Play();
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

        // Handle Chromatic Aberration for ability activation
        private void StartChromaticAberrationEffect()
        {
            StartCoroutine(SmoothChromaticAberration(abilityChromaticAberration, 0.25f));
        }

        private void ResetChromaticAberrationEffect()
        {
            StartCoroutine(SmoothChromaticAberration(originalChromaticAberration, 0.25f));
        }

        private IEnumerator SmoothChromaticAberration(float targetValue, float duration)
        {
            float elapsedTime = 0f;
            float startValue = chromaticAberration.intensity.value;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                chromaticAberration.intensity.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
                yield return null;
            }

            chromaticAberration.intensity.value = targetValue;
        }

        // Start Motion Blur and Lens Distortion effects when dashing
        private void HandleDashStarted(float dashDuration)
        {
            dashTime = dashDuration;
            StartCoroutine(SmoothMotionBlurAndLensDistortion(dashMotionBlurIntensity, dashLensDistortionIntensity, 0.15f));
        }

        // Reset Motion Blur and Lens Distortion effects after dashing
        private void HandleDashEnded()
        {
            StartCoroutine(SmoothMotionBlurAndLensDistortion(originalMotionBlurIntensity, originalLensDistortionIntensity, 0.15f));
        }

        private IEnumerator SmoothMotionBlurAndLensDistortion(float targetMotionBlur, float targetLensDistortion, float duration)
        {
            float elapsedTime = 0f;
            float startMotionBlur = motionBlur.intensity.value;
            float startLensDistortion = lensDistortion.intensity.value;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                motionBlur.intensity.value = Mathf.Lerp(startMotionBlur, targetMotionBlur, elapsedTime / duration);
                lensDistortion.intensity.value = Mathf.Lerp(startLensDistortion, targetLensDistortion, elapsedTime / duration);
                yield return null;
            }

            // Wait for dash to finish
            yield return new WaitForSeconds(dashTime);
        }
    }
}


