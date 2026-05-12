using UnityEngine;
using UnityEngine.UIElements;

public class ShowerSound : MonoBehaviour
{

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;

    }

    public void StartSound()
    {
        audioSource.Play();
    }

    public void PauseSound()
    {
        audioSource.Pause();
    }

    public void ResumeSound()
    {
        audioSource.UnPause();
    }


}
