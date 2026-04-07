using UnityEngine;

namespace PadlockSystem
{
    [RequireComponent(typeof(Camera))]
    public class PadlockInteractor : MonoBehaviour
    {
        [Header("Raycast Interact Distance")]
        [SerializeField] private float rayDistance = 5; //Distance for interaction

        [Header("Raycast Pickup Input")]
        [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

        private PadlockItem padlockItem;      // Currently targeted padlock
        private Camera _camera;               // Cached camera reference

        void Start()
        {
            // Try to get Camera component and cache it
            if (!TryGetComponent<Camera>(out _camera))
            {
                Debug.LogError("Camera component not found on the GameObject.");
            }
        }

        void Update()
        {
            // Raycast from centre of screen forward
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayDistance))
            {
                // Check if hit object has a PadlockItem component
                var padlock = hit.collider.GetComponent<PadlockItem>();

                // If padlock found, store it and highlight crosshair
                if (padlock != null)
                {
                    padlockItem = padlock;
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearSelected();
                }
            }
            else
            {
                ClearSelected();
            }

            // If a padlock is selected and interact key pressed, open it
            if (padlockItem != null)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    padlockItem.ShowPadlock();
                }
            }
        }

        private void ClearSelected()
        {
            // Remove selection if something was selected previously
            if (padlockItem != null)
            {
                HighlightCrosshair(false);
                padlockItem = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            // Update UI crosshair highlight
            PLUIManager.instance.HighlightCrosshair(on);
        }
    }
}
