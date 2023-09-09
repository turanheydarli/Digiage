using System;
using System.Collections.Generic;
using System.Linq;
using Core.Audio.Abstraction;
using Core.Audio.Music;
using Core.Managers.Audio.Music;
using Core.Utilities.Singletons;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Managers.Audio
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        public static event UnityAction<bool> OnMasterMutedChanged;

        private Dictionary<BaseAudioFile, MusicChanel> musicChanels = new Dictionary<BaseAudioFile, MusicChanel>();

        protected override void Awake()
        {
            base.Awake();

            LoadVolumeSettings();
        }

        protected virtual void OnDestroy()
        {
            SaveVolumeSettings();
        }

        public void PlayMusic<T>(T audioFile) where T : MusicFile
        {
            MusicChanel musicChanel;

            if (!musicChanels.ContainsKey(audioFile))
            {
                GameObject musicChanelGo = new GameObject(nameof(audioFile), typeof(MusicChanel));
                musicChanelGo.transform.SetParent(transform);
                musicChanel = musicChanelGo.GetComponent<MusicChanel>();
                musicChanels[audioFile] = musicChanel;
            }

            musicChanel = musicChanels[audioFile];
            musicChanel.AudioSource.clip = audioFile.GetRandomClip();
            musicChanel.AudioSource.volume = 1;
            musicChanel.AudioSource.loop = audioFile.audioLoop;
            musicChanel.AudioSource.pitch = audioFile.audioPitch;

            musicChanel.Init();
            musicChanel.Play(audioFile);
        }

        public void StopMusic<T>(T audioFile) where T : BaseAudioFile
        {
            if (!musicChanels.ContainsKey(audioFile))
            {
                Debug.LogWarning("Audio file not found in AudioManager's musicChanels dictionary.");
                return;
            }

            MusicChanel musicChanel = musicChanels[audioFile];
            musicChanel.Stop();
        }


        #region Volume Logic

        public bool MasterMuted = false;

        public float MasterVolume = 1;
        public float ModifiedMasterVolume => MasterVolume * Convert.ToInt32(!MasterMuted);

        public bool MusicMuted = false;
        public float MusicVolume = 1;
        public float ModifiedMusicVolume => ModifiedMasterVolume * MusicVolume * Convert.ToInt32(!MusicMuted);

        public bool SoundMuted = false;
        public float SoundVolume = 1;
        public float ModifiedSoundVolume => ModifiedMasterVolume * SoundVolume * Convert.ToInt32(!SoundMuted);

        public void SaveVolumeSettings()
        {
            if (!AudioSettings.Instance.SaveVolumeToPlayerPrefs) return;

            PlayerPrefs.SetFloat(AudioSettings.Instance.MasterVolumeKey, MasterVolume);
            PlayerPrefs.SetFloat(AudioSettings.Instance.MusicVolumeKey, MusicVolume);
            PlayerPrefs.SetFloat(AudioSettings.Instance.SoundVolumeKey, SoundVolume);

            PlayerPrefs.SetInt(AudioSettings.Instance.MasterMutedKey, Convert.ToInt16(MasterMuted));
            PlayerPrefs.SetInt(AudioSettings.Instance.MusicMutedKey, Convert.ToInt16(MusicMuted));
            PlayerPrefs.SetInt(AudioSettings.Instance.SoundMutedKey, Convert.ToInt16(SoundMuted));

            PlayerPrefs.Save();
        }

        public void LoadVolumeSettings()
        {
            if (!AudioSettings.Instance.SaveVolumeToPlayerPrefs) return;

            MasterVolume = PlayerPrefs.GetFloat(AudioSettings.Instance.MasterVolumeKey, 1);
            MusicVolume = PlayerPrefs.GetFloat(AudioSettings.Instance.MusicVolumeKey, 1);
            SoundVolume = PlayerPrefs.GetFloat(AudioSettings.Instance.SoundVolumeKey, 1);

            MasterMuted = Convert.ToBoolean(PlayerPrefs.GetInt(AudioSettings.Instance.MasterMutedKey, 0));
            MusicMuted = Convert.ToBoolean(PlayerPrefs.GetInt(AudioSettings.Instance.MusicMutedKey, 0));
            SoundMuted = Convert.ToBoolean(PlayerPrefs.GetInt(AudioSettings.Instance.SoundMutedKey, 0));
        }

        public void ChangeMuted()
        {
            MusicMuted = !MusicMuted;
            OnMasterMutedChanged?.Invoke(MusicMuted);
            SaveVolumeSettings();
        }

        #endregion
    }
}