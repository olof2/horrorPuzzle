using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Vector3 baseLocalposition;

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
        float insanityPercent = SanityMeter.Instance.sanityLevel / SanityMeter.Instance.maxSanityLevel;
        float intensity = insanityPercent - 0.5f; // Startar bara när sanity är under 50%
        if(intensity < 0 ) intensity = 0f; //om den är negativ så sätt den till 0


        float x = Random.Range(-0.02f, 0.02f) * intensity;
        float y = Random.Range(-0.02f, 0.02f) * intensity;

        cameraTransform.localPosition = baseLocalposition + new Vector3(x, y, 0f);
    }
}
