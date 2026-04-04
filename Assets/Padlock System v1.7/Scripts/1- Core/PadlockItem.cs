using UnityEngine;

namespace PadlockSystem
{
    public class PadlockItem : MonoBehaviour
    {
        [SerializeField] private PadlockController _padlockController = null;   // Reference to the padlock controller this item triggers

        public void ShowPadlock()
        {
            // Open the padlock UI through the controller
            _padlockController.ShowPadlock();
        }
    }
}

