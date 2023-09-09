using Core.Audio.Abstraction;
using Core.Managers.Audio.Music;
using UnityEngine;

namespace Core.Audio.Music
{
    [AddComponentMenu("")]
    [RequireComponent(typeof(AudioSource))]
    public class MusicChanel : BaseAudioChanel<MusicFile>
    {
        
    }
}