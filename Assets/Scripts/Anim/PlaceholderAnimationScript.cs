using UnityEngine;

public class PlaceholderAnimationScript : MonoBehaviour                 //script för placeholder animation
{
    private Animator animator;

    public TriggerZonePlaceholder zone;                                 //asign en "zone" till animation i inspect

    public float sanityMeterThreshold = 25f;                            //sanity meter level där animationer kan börja triggas

    private float animationCooldownTimer = 0f;                          //cooldown timer för animationer + min och max cooldown time
    public float minCooldownTime = 6f;
    public float maxCooldownTime = 16f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (SanityMeter.Instance == null) return;                       //failsafe, om singleton inte finns

        float sanityLevel = SanityMeter.Instance.sanityLevel;

        if (sanityLevel >= sanityMeterThreshold && zone.PlayerInsideZone && animationCooldownTimer <= 0f)   //animation händer om sanityLevel är större eller lika med 25, player är
        {                                                                                                   //i trigger zone, och animation cooldown är mindre eller lika med 0
            if (animator != null)
            {
                animator.SetTrigger("isFalling");

                animationCooldownTimer = Random.Range(minCooldownTime, maxCooldownTime);    //random cooldown time för animationer
            }
            else { System.Diagnostics.Debug.WriteLine("Animator is null"); }
        }

        if (animationCooldownTimer > 0f)                                //räkna ner animation cooldown (8 sec)
        {
            animationCooldownTimer -= Time.deltaTime;
        }
    }
}

