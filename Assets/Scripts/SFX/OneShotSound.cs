using UnityEngine;
using System.Collections;

public class OneShotSound : MonoBehaviour
{

    private AudioSource audioSource;
    private bool hasPlayed = false;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            
        }
    }

   

}

