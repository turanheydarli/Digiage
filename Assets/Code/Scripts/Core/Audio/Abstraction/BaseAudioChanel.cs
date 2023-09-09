using System;
using System.Collections;
using Core.Managers.Audio;
using UnityEngine;

namespace Core.Audio.Abstraction
{
    public class BaseAudioChanel<T> : MonoBehaviour where T : BaseAudioFile
    {
        private void Awake()
        {
            AudioManager.OnMasterMutedChanged += ChangeMuted;
        }

        private void ChangeMuted(bool isMuted)
        {
            AudioSource.mute = isMuted;
        }

        public AudioSource AudioSource { get; private set; }

        private Coroutine fadeCoroutine;

        public void Init()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.volume = AudioManager.Instance.ModifiedMasterVolume;
            enabled = false;
        }

        public void Play(T audioFile)
        {
            if (audioFile == null)
                return;

            PlayAudioClip(audioFile.GetRandomClip());
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        public void Fade(float targetVolume, float duration)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeCoroutine(targetVolume, duration));
        }

        private void PlayAudioClip(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }

        private void PlayAudioClipAtPosition(AudioClip clip, Vector3 position)
        {
            AudioSource.clip = clip;
            AudioSource.spatialBlend = 1.0f;
            AudioSource.minDistance = 1.0f;
            // AudioSource.maxDistance = audioFile.maxDistance;
            AudioSource.transform.position = position;
            AudioSource.Play();
        }

        private IEnumerator FadeCoroutine(float targetVolume, float duration)
        {
            float startVolume = AudioSource.volume;
            float startTime = Time.time;

            while (Time.time - startTime < duration)
            {
                float elapsed = Time.time - startTime;
                float t = Mathf.Clamp01(elapsed / duration);
                AudioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
                yield return null;
            }

            AudioSource.volume = targetVolume;
            fadeCoroutine = null;
        }
    }
}