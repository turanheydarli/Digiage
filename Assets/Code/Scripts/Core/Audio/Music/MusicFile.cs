using Core.Audio.Abstraction;
using UnityEngine;

namespace Core.Managers.Audio.Music
{
    [CreateAssetMenu(fileName = "New Music File", menuName = "AudioManager/Music File", order = 1)]
    public class MusicFile : BaseAudioFile
    {
        public void Play(bool isMain)
        {
            // AudioManager.Instance.PlayMusicInternal(this, isMain);
        }

        public void Play(Transform transform = null)
        {
            //  AudioManager.Instance.PlayMusicInternal(this, transform, helper);
        }
    }
}