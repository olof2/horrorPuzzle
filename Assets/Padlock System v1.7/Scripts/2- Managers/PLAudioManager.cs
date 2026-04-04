using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace PadlockSystem
{
    public class PLAudioManager : MonoBehaviour
    {
        [Header("List of Sound Effect SO's")]
        [SerializeField] private Sound[] sounds = null;                 // Array of Sound ScriptableObjects

        [Header("Sound Mixer Group")]
        [SerializeField] private AudioMixerGroup mixerGroup = null;     // Mixer group to route audio through

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;       // Persist manager across scene loads

        public static PLAudioManager instance;

        void Awake()
        {
            // Enforce singleton pattern
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;

                // Make manager persistent if enabled
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }

            // Create AudioSources for each sound definition
            foreach (Sound s in sounds)
            {
                // Add a new AudioSource for each sound
                s.source = gameObject.AddComponent<AudioSource>();

                // Assign clip and loop settings
                s.source.clip = s.clip;
                s.source.loop = s.loop;

                // Route audio through assigned mixer group
                s.source.outputAudioMixerGroup = mixerGroup;
            }
        }

        public void Play(Sound sound)
        {
            // Find matching sound entry in array
            Sound s = sounds.FirstOrDefault(item => item == sound);

            // If sound not found, warn and exit
            if (s == null)
            {
                Debug.LogWarning("Sound: " + sound + " not found!");
                return;
            }

            // Apply randomized volume variance
            s.source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));

            // Apply randomized pitch variance
            s.source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            // Play the audio source
            s.source.Play();
        }

        public void StopPlaying(Sound sound)
        {
            // Find matching sound entry in array
            Sound s = sounds.FirstOrDefault(item => item == sound);

            // If sound not found, warn and exit
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            // Apply final volume and pitch variance before stopping
            s.source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            // Stop the audio source
            s.source.Stop();
        }
    }
}
