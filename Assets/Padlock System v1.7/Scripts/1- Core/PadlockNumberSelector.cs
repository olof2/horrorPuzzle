using UnityEngine;
using UnityEngine.EventSystems;

namespace PadlockSystem
{
    public class PadlockNumberSelector : MonoBehaviour, IPointerClickHandler
    {
        [Header("Padlock Row")]
        [SerializeField] private PadlockRow selectedRow = PadlockRow.row1;   // Which dial this selector controls
        private enum PadlockRow { row1, row2, row3, row4 }

        private int spinnerNumber;       // Current number shown on this dial
        private int spinnerLimit;        // Max number before wrapping
        private PadlockController _padlockController; // Reference to controller

        private void Awake()
        {
            // Initialise spinner starting value
            spinnerNumber = 1;

            // Highest selectable number before looping
            spinnerLimit = 9;
        }

        // Called from PadlockController.SpawnPadlock to wire controller reference
        public void UpdatePadlockController(PadlockController newController)
        {
            _padlockController = newController;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"Clicked on {gameObject.name} - rotating dial and updating controller");

            // Rotate dial to next number
            RotateSpinner();

            // Update controller with new number
            ApplyValueToController();

            // Ask controller to check if combination is now correct (guarded)
            if (_padlockController != null)
                _padlockController.CheckCombination();
            else
                Debug.LogWarning($"{nameof(PadlockNumberSelector)}: _padlockController is null on click for {gameObject.name}");
        }

        void RotateSpinner()
        {
            // Increment spinner and wrap after reaching limit
            spinnerNumber = (spinnerNumber % spinnerLimit) + 1;

            // Rotate the visual dial - rotate by a fixed amount around the local Z axis
            transform.Rotate(Vector3.forward * 40f, Space.Self);

            // Play dial spin audio if controller is available
            if (_padlockController != null)
                _padlockController.SpinSound();
            else
                Debug.LogWarning($"{nameof(PadlockNumberSelector)}: attempted to play spin sound but _padlockController is null for {gameObject.name}");
        }

        void ApplyValueToController()
        {
            if (_padlockController == null)
            {
                Debug.LogWarning($"{nameof(PadlockNumberSelector)}: controller reference missing for {gameObject.name}");
                return;
            }

            // Cache updated value
            int updatedRowValue = spinnerNumber;

            // Apply updated spinner value to matching row in controller
            switch (selectedRow)
            {
                case PadlockRow.row1:
                    _padlockController.combinationRow1 = updatedRowValue;
                    break;
                case PadlockRow.row2:
                    _padlockController.combinationRow2 = updatedRowValue;
                    break;
                case PadlockRow.row3:
                    _padlockController.combinationRow3 = updatedRowValue;
                    break;
                case PadlockRow.row4:
                    _padlockController.combinationRow4 = updatedRowValue;
                    break;
            }
        }
    }
}
