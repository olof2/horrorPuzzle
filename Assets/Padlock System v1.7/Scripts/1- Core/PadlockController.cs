using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PadlockSystem
{
    public class PadlockController : MonoBehaviour
    {
        [Header("Padlock Code")]
        [SerializeField] private string yourCombination = null;        // The correct combination to unlock

        [Header("Interactive Padlock")]
        [SerializeField] private GameObject interactableLock = null;  // World object to disable once unlocked

        [Header("Prefab To Spawn")]
        [SerializeField] private GameObject padlockPrefab = null;     // Inspectable padlock prefab

        [Header("Spawn Distance")]
        [SerializeField] private float distanceFromCamera = 0.3f;     // How far in front of the camera to show the padlock

        [Header("Animation Name")]
        [SerializeField] private string lockOpen = "LockOpen";         // Animation to play when unlocked

        [Header("Raycast Pickup Input")]
        [SerializeField] private KeyCode closeKey;                    // Input to close/exit padlock view

        [Header("Audio Names")]
        [SerializeField] private Sound padlockInteractSound = null;   // Sound when opening padlock view
        [SerializeField] private Sound padlockSpinSound = null;       // Sound for dial rotation
        [SerializeField] private Sound padlockUnlockSound = null;     // Sound when padlock unlocks

        [Header("Trigger Type - ONLY if using a trigger event")]
        [SerializeField] private bool isPadlockTrigger = false;       // If tied to a trigger prompt
        [SerializeField] private GameObject triggerObject = null;     // Trigger object to disable/enable

        [Header("Unlock Events")]
        [SerializeField] private UnityEvent unlock = null;            // Events to fire when unlocked

        public int combinationRow1 { get; set; }                      // Dial 1 value
        public int combinationRow2 { get; set; }                      // Dial 2 value
        public int combinationRow3 { get; set; }                      // Dial 3 value
        public int combinationRow4 { get; set; }                      // Dial 4 value

        private string playerCombi;                                   // Combined player input
        private bool hasUnlocked;                                     // Prevents multiple triggers
        private bool isShowing;                                       // True when padlock UI is visible
        private Camera mainCamera;                                    // Cached main camera reference
        private Animator lockAnim;                                    // Animator on spawned padlock
        private GameObject instantiatedPadlock;                       // Spawned padlock reference

        void Awake()
        {
            // Cache main camera
            mainCamera = Camera.main;

            // Initialise dial values
            combinationRow1 = 1;
            combinationRow2 = 1;
            combinationRow3 = 1;
            combinationRow4 = 1;
        }

        void Update()
        {
            // Prevent closing unless padlock is visible
            if (isShowing && Input.GetKeyDown(closeKey))
            {
                DisablePadlock();
            }
        }

        void UnlockPadlock()
        {
            // Fire UnityEvent for unlocked state
            unlock.Invoke();
            Debug.Log("Here");
        }

        public void ShowPadlock()
        {
            // Mark padlock UI as active
            isShowing = true;

            // Disable player controller
            PLDisableManager.instance.DisablePlayer(true);

            // Spawn padlock in front of camera
            SpawnPadlock(distanceFromCamera);

            // Reset camera roll (sometimes needed)
            mainCamera.transform.localEulerAngles = new Vector3(0, 0, 0);

            // Play interaction sound
            InteractSound();

            // Trigger-based interaction handling
            if (isPadlockTrigger)
            {
                PLUIManager.instance.ShowUIPrompt(false);
                triggerObject.SetActive(false);
            }
        }

        void SpawnPadlock(float distance)
        {
            // Create padlock prefab as child of camera
            GameObject padlockInstance = Instantiate(padlockPrefab, mainCamera.transform);

            // Position padlock directly in front of camera
            padlockInstance.transform.localPosition = new Vector3(0, 0, distance);

            // Rotate so the front faces player
            padlockInstance.transform.localRotation = Quaternion.Euler(0, 90, 0);

            // Store reference
            instantiatedPadlock = padlockInstance;

            // Cache animator
            lockAnim = padlockInstance.GetComponentInChildren<Animator>();

            // Find all dial selector components
            PadlockNumberSelector[] numberSelectors = padlockInstance.GetComponentsInChildren<PadlockNumberSelector>();

            // Assign this controller to each selector
            foreach (PadlockNumberSelector selector in numberSelectors)
            {
                selector.UpdatePadlockController(this);
            }
        }

        void DisablePadlock()
        {
            // Mark UI as not showing
            isShowing = false;

            // Re-enable player
            PLDisableManager.instance.DisablePlayer(false);

            // Remove padlock instance
            Destroy(instantiatedPadlock);

            // Restore interaction prompt if using trigger mode
            if (isPadlockTrigger)
            {
                PLUIManager.instance.ShowUIPrompt(true);
                triggerObject.SetActive(true);
            }
        }

        public void CheckCombination()
        {
            // Build player string from all 4 dials
            playerCombi =
                combinationRow1.ToString("0") +
                combinationRow2.ToString("0") +
                combinationRow3.ToString("0") +
                combinationRow4.ToString("0");

            // Check if correct and hasn't already unlocked
            if (playerCombi == yourCombination)
            {
                if (!hasUnlocked)
                {
                    StartCoroutine(CorrectCombination());
                    hasUnlocked = true;
                }
            }
        }

        IEnumerator CorrectCombination()
        {
            // Play unlocking animation
            lockAnim.Play(lockOpen);

            // Play unlock audio
            UnlockSound();

            // Wait for animation to finish
            const float waitDuration = 1.2f;
            yield return new WaitForSeconds(waitDuration);

            // Clean up padlock UI
            Destroy(instantiatedPadlock);

            // Disable world lock object
            interactableLock.SetActive(false);

            // Fire unlock event
            UnlockPadlock();

            // Hide trigger prompt if using trigger
            if (isPadlockTrigger)
            {
                PLUIManager.instance.ShowUIPrompt(false);
                triggerObject.SetActive(false);
            }

            // Restore player movement
            PLDisableManager.instance.DisablePlayer(false);

            // Disable this controller after unlock
            gameObject.SetActive(false);
        }

        void InteractSound()
        {
            // Play padlock interaction sound
            PLAudioManager.instance.Play(padlockInteractSound);
        }

        public void SpinSound()
        {
            // Play number dial spinning sound
            PLAudioManager.instance.Play(padlockSpinSound);
        }

        public void UnlockSound()
        {
            // Play padlock unlock sound
            PLAudioManager.instance.Play(padlockUnlockSound);
        }
    }
}
