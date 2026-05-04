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
        float intensity = insanityPercent;

        float x = Random.Range(-0.1f, 0.1f) * intensity;
        float y = Random.Range(-0.1f, 0.1f) * intensity;

        cameraTransform.localPosition = baseLocalposition + new Vector3(x, y, 0f);
    }
}
