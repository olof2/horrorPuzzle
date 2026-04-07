using UnityEngine;
using UnityEngine.Events;

namespace PadlockSystem
{
    public class PLDisableManager : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent OnPlayerDisabled;    // Fired when player is disabled
        [SerializeField] private UnityEvent OnPlayerEnabled;     // Fired when player is re-enabled

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true; // Keep this manager when loading new scenes

        public static PLDisableManager instance;

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

                // Make persistent if enabled
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        public void DisablePlayer(bool disable)
        {
            // Invoke appropriate event based on disable state
            if (disable)
            {
                OnPlayerDisabled?.Invoke();
            }
            else
            {
                OnPlayerEnabled?.Invoke();
            }
        }
    }
}
