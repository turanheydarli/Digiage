using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio.Abstraction
{
    public abstract class BaseAudioFile : ScriptableObject
    {
        [SerializeField] protected List<AudioClip> files = new List<AudioClip>();

        public string audioName;
        public bool audioLoop;
        public float audioPitch;

        public List<AudioClip> Clips
        {
            get { return files; }
        }

        public AudioClip GetRandomClip()
        {
            return files[Random.Range(0, files.Count)];
        }
    }
}