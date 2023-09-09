using Core.Utilities.ScriptableObjects;
using UnityEngine;

namespace Core.Managers.Audio
{
    [CreateAssetMenu(fileName = "New AudioSettings", menuName = "AudioManager/Audio Settings")]
    public class AudioSettings : ScriptableObjectSingleton<AudioSettings>
    {
        [SerializeField] private bool saveVolumeToPlayerPrefs;
        
        public bool SaveVolumeToPlayerPrefs => saveVolumeToPlayerPrefs;
        public string MasterVolumeKey => "PREFS_MasterVolume";
        public string MusicVolumeKey => "PREFS_MusicVolume";
        public string SoundVolumeKey => "PREFS_SoundVolume";

        public string MasterMutedKey => "PREFS_MasterMuted";
        public string MusicMutedKey => "PREFS_MusicMuted";
        public string SoundMutedKey => "PREFS_SoundMuted";
    }
}