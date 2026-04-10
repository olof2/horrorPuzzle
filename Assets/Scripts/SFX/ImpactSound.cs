using UnityEngine;

public class ImpactSound : MonoBehaviour
{

    private AudioSource audioSource;
    private float minVelocity = 2f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > minVelocity)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
                
        }

    }


}
