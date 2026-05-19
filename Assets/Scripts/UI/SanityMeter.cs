using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;


public class SanityMeter : Singleton<SanityMeter>
{
    private SanityMeterUI sanityMeterUI;
    private GameOverScript gameOverScript;

    public float sanityLevel = 0f;
    public float endMusicStartThreshold = 550f;
    [SerializeField] public float maxSanityLevel = 100f;
    [SerializeField] public float increaseRate = 0f; // Rate at which sanity decreases per second

    private AudioSource audioSource;
    public AudioClip sanitySound10;
    public AudioClip sanitySound25;
    public AudioClip sanitySound50;
    public AudioClip sanitySound75;
    public AudioClip sanitySound95;
    public AudioClip increaseSound100;

    //Event Action för varje threashold (25 -> 50 -> 75 -> 100)
    public event Action OnReached10;
    public event Action OnReached25;
    public event Action OnReached50;
    public event Action OnReached75;
    public event Action OnReached95;
    public event Action OnReached100;

    // Så det bara spelas en gång
    private bool hasPlayed10 = false;
    private bool hasPlayed25 = false;
    private bool hasPlayed50 = false;
    private bool hasPlayed75 = false;
    private bool hasPlayed95 = false;
    private bool hasPlayed100 = false;
    private bool hasPlayedEndMusic = false;

    // bool för om event Action är skickat än eller inte
    private bool sent10;
    private bool sent25;
    private bool sent50;
    private bool sent75;
    private bool sent95;
    private bool sent100;

    void Start()
    {
        // Find the UIDocument in the scene and get the SanityMeterUI component
        var uiDocument = FindFirstObjectByType<UIDocument>();
        sanityMeterUI = uiDocument.rootVisualElement.Q<SanityMeterUI>();
        

        if (sanityMeterUI != null)
        sanityMeterUI.sanityLevel = sanityLevel; // Initialize the UI with the starting sanity level
        

        sanityLevel = Mathf.Clamp(sanityLevel, 0f, maxSanityLevel); // Ensure sanity level starts within bounds

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        sanityLevel += Time.deltaTime * increaseRate; // Increase sanity level over time (adjust the rate as needed)
        sanityLevel = Mathf.Clamp(sanityLevel, 0f, maxSanityLevel); // Clamp sanity level between 0 and max

        //if (sanityLevel == 100f)
        //{
        //    gameOverScript.GameOver();  
        //    Debug.Log("Sanity is at maximum!");
        //}

        if (sanityLevel >= endMusicStartThreshold && !hasPlayedEndMusic)
        {
            if (MusicSystem.Instance != null)
            {
                MusicSystem.Instance.Play("Intro");
                hasPlayedEndMusic = true;
            }    
        }

        if (sanityLevel >= maxSanityLevel * 0.1f && !hasPlayed10)
        {
            audioSource.PlayOneShot(sanitySound10);
            hasPlayed10 = true; 
        }

        if (sanityLevel >= maxSanityLevel * 0.25f && !hasPlayed25)
        {
            audioSource.PlayOneShot(sanitySound25);
            hasPlayed25 = true; // 
        }

        if(sanityLevel >= maxSanityLevel * 0.50f && !hasPlayed50)
        {
            audioSource.PlayOneShot(sanitySound50);
            hasPlayed50 = true; 
        }

        if (sanityLevel >= maxSanityLevel * 0.75f && !hasPlayed75)
        {
            audioSource.PlayOneShot(sanitySound75);
            hasPlayed75 = true; 
        }

        //if (sanityLevel >= maxSanityLevel * 0.95f && !hasPlayed95)
        //{
        //    audioSource.PlayOneShot(sanitySound95);
        //    hasPlayed95 = true; 
        //}

        if(sanityLevel >= maxSanityLevel && !hasPlayed100)
        {
            audioSource.PlayOneShot(increaseSound100);
            hasPlayed100 = true; 
        }


        if (!sent10 && sanityLevel >= maxSanityLevel * 0.1f)
        {
            sent10 = true;
            OnReached10?.Invoke();

            Debug.Log("OnRechead10 succes");
        }
        if (!sent25 && sanityLevel >= maxSanityLevel * 0.25f)
        {
            sent25 = true;
            OnReached25?.Invoke();

            Debug.Log("OnRechead25 succes");
        }
        if (!sent50 && sanityLevel >= maxSanityLevel * 0.50f)
        {
            sent50 = true;
            OnReached50?.Invoke();

            Debug.Log("OnRechead50 succes");
        }
        if (!sent75 && sanityLevel >= maxSanityLevel * 0.75f)
        {
            sent75 = true;
            OnReached75?.Invoke();

            Debug.Log("OnRechead75 succes");
        }
        if (!sent95 && sanityLevel >= maxSanityLevel * 0.95f)
        {
            sent95 = true;
            OnReached95?.Invoke();

            Debug.Log("OnRechead95 succes");
        }
        if (!sent100 && sanityLevel >= maxSanityLevel)
        {
            sent100 = true;
            OnReached100?.Invoke();

            Debug.Log("OnRechead100 succes");
        }

    }
}


