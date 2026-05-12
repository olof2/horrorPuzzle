using Unity.VisualScripting;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    private AudioSource audioSource;
  

    public float fadeSpeed = 0.05f;

    private bool fadeOut = false;
    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume = 1.0f;
        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
        audioSource.loop = true;

        //audioSource.Play();
        hasStarted = false;
        //audioSource.Stop();

    }

    public void StartAmbience()
    {
        if(!hasStarted)
        {
            audioSource.Play();
            hasStarted = true;
        }
    }

    public void PauseAmbience()
    {
        if(audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void ResumeAmbience()
    {
        if(audioSource != null)
        {
            audioSource.UnPause();
        }
    }





     void Update()
    {
        if(SanityMeter.Instance != null)
        {
            float current = SanityMeter.Instance.sanityLevel;
            float max = SanityMeter.Instance.maxSanityLevel;

            if(current >= max *0.5f)
            {
                fadeOut = true;
            }
        }
      
        if(fadeOut)
        {
            if(audioSource.volume > 0)
            {
                audioSource.volume -=Time.deltaTime * fadeSpeed;
            }
            else
            {
                audioSource.Stop();
                fadeOut =false;
            }
            
        }
     
        
    }





}
