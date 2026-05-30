using UnityEngine;

public class SoundTriggerLib : MonoBehaviour
{
    public AudioSource soundSource;

    private static bool soundPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !soundPlayed)
        {
            soundSource.Play();
            soundPlayed = true;
        }
    }
   

}
