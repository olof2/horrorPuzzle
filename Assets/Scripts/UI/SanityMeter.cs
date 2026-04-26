using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;


public class SanityMeter : Singleton<SanityMeter>
{
    private SanityMeterUI sanityMeterUI;
    private GameOverScript gameOverScript;

    public float sanityLevel = 0f;
    [SerializeField] public float maxSanityLevel = 100f;
    [SerializeField] public float increaseRate = 0f; // Rate at which sanity decreases per second

    private AudioSource audioSource;
    public AudioClip sanitySound25;
    public AudioClip sanitySound50;
    public AudioClip sanitySound75;
    public AudioClip increaseSound100;

    private bool hasPlayed25 = false; // Så det bara spelas en gång
    private bool hasPlayed50 = false;
    private bool hasPlayed75 = false;
    private bool hasPlayed100 = false;

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
        if(sanityLevel >= 25f && !hasPlayed25)
        {
            audioSource.PlayOneShot(sanitySound25);
            hasPlayed25 = true; // 
        }

        if(sanityLevel >= 50f && !hasPlayed50)
        {
            audioSource.PlayOneShot(sanitySound50);
            hasPlayed50 = true; 
        }

        if (sanityLevel >= 75f && !hasPlayed75)
        {
            audioSource.PlayOneShot(sanitySound75);
            hasPlayed75 = true; 
        }

        if(sanityLevel >= 100f && !hasPlayed100)
        {
            audioSource.PlayOneShot(increaseSound100);
            hasPlayed100 = true; 
        }
    }
}


