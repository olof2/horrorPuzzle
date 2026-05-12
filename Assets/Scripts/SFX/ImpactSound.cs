using UnityEngine;

public class ImpactSound : MonoBehaviour
{

    private AudioSource audioSource;

    public void Start()
    {
         audioSource = GetComponent<AudioSource>();
    }

    public void PlayImpact()
    {
        Debug.Log("Playing impact sound");
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void StopAudio()
    {
        if(audioSource != null)
        {
            audioSource.Stop();
        }
    }
   


 


}
