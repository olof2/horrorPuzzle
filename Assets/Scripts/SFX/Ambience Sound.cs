using Unity.VisualScripting;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    private AudioSource audioSource;
  

    public float stopRainAtSanity = 50f;
    public float fadeSpeed = 0.05f;

    private bool shouldFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume = 1.0f;
        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
        audioSource.loop = true;

        audioSource.Play();

    }



     void Update()
    {
        if(SanityMeter.Instance != null &&
            SanityMeter.Instance.sanityLevel >= stopRainAtSanity)
        {
            shouldFadeOut = true;
        }

        if(shouldFadeOut)
        {
            if(audioSource.volume > 0)
            {
                audioSource.volume -=Time.deltaTime * fadeSpeed;
            }
            else
            {
                audioSource.Stop();
                shouldFadeOut =false;
            }
            
        }
     
        
    }





}
