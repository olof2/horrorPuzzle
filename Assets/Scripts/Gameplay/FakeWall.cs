using PadlockSystem;
using UnityEngine;

public class FakeWall : MonoBehaviour
{

    [SerializeField] private PadlockController padlockController;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        meshRenderer.enabled = true;
        meshCollider.enabled = true;

        if (padlockController != null)
        {
            // Prenumerera på PadlockController's CorrectCode event
            padlockController.CorrectCode += ActivateCorrectCode;
            padlockController.WrongCode += ActivateWrongCode;
        }

    }

    void Update()
    {
        
    }

    private void ActivateCorrectCode()
    {
        
    }

    private void ActivateWrongCode()
    {
        meshCollider.enabled = false;
        meshRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        if (padlockController != null)
        {
            // Avprenumerera från PadlockController's events för att undvika minnesläckor
            padlockController.CorrectCode -= ActivateCorrectCode;
            padlockController.WrongCode -= ActivateWrongCode;
        }
    }


}
