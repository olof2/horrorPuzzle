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
        audioSource.Play(); 
    }
   


 


}
