using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Vector3 baseLocalposition;

    //bool för när jumpscare händer, blir true och höjer screen shake
    private bool jumpscareShake = false;

    void Start()
    {
        baseLocalposition = cameraTransform.localPosition;
    }

    private void Update()
    {
        Shaking();
    }

    void Shaking()
    {
        //om jumscare händer, kör jumpscare values, else bara klr shake some vanligt
        if (jumpscareShake)
        {
            float insanityPercent = SanityMeter.Instance.sanityLevel / SanityMeter.Instance.maxSanityLevel;

            float intensity = insanityPercent - 0.5f; // Startar bara när sanity är under 50%
            if (intensity < 0) intensity = 0f; //om den är negativ så sätt den till 0


            float x = Random.Range(-0.02f, 0.02f) * (intensity * 10f);
            float y = Random.Range(-0.02f, 0.02f) * (intensity * 10f);

            cameraTransform.localPosition = baseLocalposition + new Vector3(x, y, 0f);
        }
        else
        {
            float insanityPercent = SanityMeter.Instance.sanityLevel / SanityMeter.Instance.maxSanityLevel;

            float intensity = insanityPercent - 0.5f; // Startar bara när sanity är under 50%
            if (intensity < 0) intensity = 0f; //om den är negativ så sätt den till 0


            float x = Random.Range(-0.02f, 0.02f) * intensity;
            float y = Random.Range(-0.02f, 0.02f) * intensity;

            cameraTransform.localPosition = baseLocalposition + new Vector3(x, y, 0f);
        }
    }

    //bool metod för jumpscare screenshake
    //när metoden anropas med "true" i parameter inne i enemy script, jumpscare shake händer
    public void SetJumpscareShake(bool activated)
    {
        jumpscareShake = activated;
    }
}
