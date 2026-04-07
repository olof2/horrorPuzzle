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

        public void UpdatePadlockController(PadlockController newController)
        {
            // Store reference to main padlock controller
            _padlockController = newController;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Rotate dial to next number
            RotateSpinner();

            // Update controller with new number
            UpdatePadlockController();

            // Ask controller to check if combination is now correct
            _padlockController.CheckCombination();
        }

        void RotateSpinner()
        {
            // Increment spinner and wrap after reaching limit
            spinnerNumber = (spinnerNumber % spinnerLimit) + 1;

            // Rotate the visual dial
            transform.Rotate(0, 0, transform.rotation.z + 40);

            // Play dial spin audio
            _padlockController.SpinSound();
        }

        void UpdatePadlockController()
        {
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
