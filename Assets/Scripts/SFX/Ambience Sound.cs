using Unity.VisualScripting;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    private AudioSource audioSource;
    public Collider Area;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume = 1.0f;
        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
        audioSource.loop = true;

        //audioSource.Play();

    }


   
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered:" + other.name);
       if(other.CompareTag("Player"))
        {
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.Stop();
        }

    }

}
