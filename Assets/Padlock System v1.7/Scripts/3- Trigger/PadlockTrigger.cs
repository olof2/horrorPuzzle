using UnityEngine;

namespace PadlockSystem
{
    public class PadlockTrigger : MonoBehaviour
    {
        [Header("Padlock Controller Object")]
        [SerializeField] private PadlockController padlockController = null;

        [Header("Trigger Inputs")]
        [SerializeField] private KeyCode triggerInteractKey = KeyCode.E;

        [Header("Player Tag")]
        [SerializeField] private const string playerTag = "Player";

        private bool canUse;    // True when player is inside trigger area

        private void Update()
        {
            // Check if player can interact and presses the interact key
            ShowPadlockInput();
        }

        void ShowPadlockInput()
        {
            // Only open padlock if inside trigger and key is pressed
            if (canUse && Input.GetKeyDown(triggerInteractKey))
            {
                padlockController.ShowPadlock();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if player entered the trigger zone
            if (other.CompareTag(playerTag))
            {
                canUse = true;

                // Show interact UI prompt
                PLUIManager.instance.ShowUIPrompt(canUse);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Check if player left the trigger area
            if (other.CompareTag(playerTag))
            {
                canUse = false;

                // Hide interact UI prompt
                PLUIManager.instance.ShowUIPrompt(canUse);
            }
        }
    }
}
