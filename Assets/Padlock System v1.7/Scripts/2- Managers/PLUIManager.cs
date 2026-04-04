using UnityEngine;
using UnityEngine.UI;

namespace PadlockSystem
{
    public class PLUIManager : MonoBehaviour
    {
        public static PLUIManager instance;

        [Header("Crosshair")]
        [SerializeField] private Image crosshair = null;              // UI crosshair graphic

        [Header("UI Prompt")]
        [SerializeField] private GameObject interactPrompt = null;    // Prompt shown when interactable is nearby

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;     // Keep UI manager between scenes

        void Awake()
        {
            // Enforce singleton pattern
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;

                // Ensure manager persists across loads if enabled
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }

            // Check inspector references for safety
            FieldNullCheck();
        }

        public void ShowUIPrompt(bool on)
        {
            // Enable or disable the interact prompt
            interactPrompt.SetActive(on);
        }

        public void DisableCrosshair(bool on)
        {
            // Toggle crosshair visibility
            crosshair.enabled = !on;

            // Unlock cursor when UI takes over
            Cursor.lockState = on ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = on;
        }

        public void HighlightCrosshair(bool on)
        {
            // Change crosshair colour based on whether target is interactable
            crosshair.color = on ? Color.red : Color.white;
        }

        void FieldNullCheck()
        {
            // Verify required fields exist
            CheckField(crosshair, "Crosshair");
            CheckField(interactPrompt, "InteractPrompt");
        }

        void CheckField(Object field, string fieldName)
        {
            // Log error if field has not been assigned in Inspector
            if (field == null)
            {
                Debug.LogError($"FieldNullCheck: {fieldName} is not set in the inspector!");
            }
        }
    }
}
