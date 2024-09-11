using Chapter6;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FusionFuryGame
{
    public class AudioManager : Singlton<AudioManager>
    {
        public GameSettings gameSettings;
        public AudioMixer audioMixer;
        public AudioMixerGroup sfxGroup;
        public AudioMixerGroup musicGroup;
        public AudioClip btnClick;
        private AudioSource musicSource;
        public float fadeDuration = 1.0f; // Duration of the fade transition
        public int poolSize = 10;

        public AudioSource audioSourcePrefab;

        private List<AudioSource> pool = new List<AudioSource>();

        protected override void Awake()
        {
            base.Awake();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.outputAudioMixerGroup = musicGroup;
            musicSource.loop = true;

            for (int i = 0; i < poolSize; i++)
            {
                AudioSource audioSource = CreateAudioSource();
                pool.Add(audioSource);
            }


        }
        private void Start()
        {
            SetupMixer();
        }
        private void SetupMixer()
        {
            audioMixer.SetFloat("SFX", gameSettings.isSoundOn ? Mathf.Log10(1) * 20 : -80);
            audioMixer.SetFloat("Music", gameSettings.isMusicOn ? Mathf.Log10(1) * 20 : -80);
        }

        public void PlayMusic(AudioClip clip, float volume = 1f)
        {

            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }

        public void SetMasterVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }

        public void SetSFXVolume(float volume)
        {
            audioMixer.SetFloat("SFX", volume > 0 ? Mathf.Log10(volume) * 20 : -80);
            gameSettings.isSoundOn = volume == 1;
        }

        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat("Music", volume > 0 ? Mathf.Log10(volume) * 20 : -80);
            gameSettings.isMusicOn = volume == 1;
        }

        public void PauseMusic()
        {
            musicSource.Pause();
        }

        public void ResumeMusic()
        {
            musicSource.UnPause();
        }

        public void PlaySFX(AudioClip audioClip, float volume = 1f)
        {
            AudioSource audioSource = GetAvailableAudioSource();
            if (audioSource != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                StartCoroutine(ReturnAudioSourceToPoolAfterPlaying(audioSource));
            }
        }

        private AudioSource GetAvailableAudioSource()
        {
            foreach (AudioSource audioSource in pool)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }
            return null;
        }

        private IEnumerator ReturnAudioSourceToPoolAfterPlaying(AudioSource audioSource)
        {
            while (audioSource.isPlaying)
            {
                yield return null;
            }
            audioSource.clip = null;
            audioSource.Stop();
        }

        private AudioSource CreateAudioSource()
        {
            AudioSource audioSource = Instantiate(audioSourcePrefab, transform);
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.outputAudioMixerGroup = sfxGroup;
            audioSource.spatialBlend = 0f;
            return audioSource;
        }


        public void PlayButtonSound()
        {
            PlaySFX(btnClick);
        }


        private IEnumerator FadeIn(AudioSource source, float duration, float targetVolume)
        {
            source.UnPause();
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(0, targetVolume, t / duration);
                yield return null;
            }
            source.volume = targetVolume;
        }

        public void FadeOutMusic(float duration)
        {
            StartCoroutine(FadeOut(musicSource, duration));
        }

        private IEnumerator FadeOut(AudioSource source, float duration)
        {
            float startVolume = source.volume;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, 0, t / duration);
                yield return null;
            }
            source.volume = 0;
            source.Pause();
        }

        public void FadeInMusic(float duration)
        {
            StartCoroutine(FadeIn(musicSource, duration, 0.5f));
        }
    }
}