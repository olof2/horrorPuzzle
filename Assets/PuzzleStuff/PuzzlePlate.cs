using UnityEngine;

enum PlateRotationState { rotate_0, rotate_90, rotate_180, rotate_270, rotate_360 }

[RequireComponent(typeof(Collider))]
public class PuzzlePlate : MonoBehaviour, I_Interactable
{
    [SerializeField]
    Transform plateTransform;

    [SerializeField]
    float rotationSpeed = 180f; // degrees per second

    [SerializeField]
    float snapThreshold = 0.5f; // degrees

    [SerializeField]
    PlateRotationState rotationState = PlateRotationState.rotate_0;

    bool isRotating = false;

    Quaternion targetRotation = Quaternion.identity;

    void Start()
    {
        if (plateTransform == null) plateTransform = this.transform;
        // Ensure transform starts at rotationState
        float startY = StateToAngle(rotationState);
        plateTransform.rotation = Quaternion.Euler(0f, startY, 0f);
        targetRotation = plateTransform.rotation;
    }

    void Update()
    {
        if (isRotating)
        {
            plateTransform.rotation = Quaternion.RotateTowards(plateTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float angleDiff = Quaternion.Angle(plateTransform.rotation, targetRotation);
            if (angleDiff <= snapThreshold)
            {
                // Snap exactly to target and finish rotation
                plateTransform.rotation = targetRotation;
                isRotating = false;

                // Update rotationState to the nearest 90-degree step based on final Y angle
                float finalY = NormalizeAngle(plateTransform.eulerAngles.y);
                float snapped = Mathf.Round(finalY / 90f) * 90f;
                snapped = NormalizeAngle(snapped);

                if (Mathf.Approximately(snapped, 0f) || Mathf.Approximately(snapped, 360f))
                    rotationState = PlateRotationState.rotate_0;
                else if (Mathf.Approximately(snapped, 90f))
                    rotationState = PlateRotationState.rotate_90;
                else if (Mathf.Approximately(snapped, 180f))
                    rotationState = PlateRotationState.rotate_180;
                else if (Mathf.Approximately(snapped, 270f))
                    rotationState = PlateRotationState.rotate_270;
                else
                    rotationState = PlateRotationState.rotate_0;
            }
        }
        else
        {
            // Keep transform aligned with rotationState when idle
            float targetY = StateToAngle(rotationState);
            plateTransform.rotation = Quaternion.Euler(0f, targetY, 0f);
            targetRotation = plateTransform.rotation;
        }
    }

    public void OnRotate()
    {
        if (isRotating) return; // ignore input while rotating

        // Compute next target: snap current angle to nearest 90 and add 90
        float currentY = NormalizeAngle(plateTransform.eulerAngles.y);
        float snapped = Mathf.Round(currentY / 90f) * 90f;
        float nextAngle = snapped + 90f;
        // Keep nextAngle normalized to reasonable value (can be >360, Quaternion handles it)
        targetRotation = Quaternion.Euler(0f, nextAngle, 0f);
        isRotating = true;
        // rotationState will be updated when rotation finishes
    }

    // I_Interactable implementation: forward interact to rotation
    public void Interact()
    {
        OnRotate();
    }

    static float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f) angle += 360f;
        return angle;
    }

    static float StateToAngle(PlateRotationState state)
    {
        switch (state)
        {
            case PlateRotationState.rotate_0: return 0f;
            case PlateRotationState.rotate_90: return 90f;
            case PlateRotationState.rotate_180: return 180f;
            case PlateRotationState.rotate_270: return 270f;
            case PlateRotationState.rotate_360: return 360f;
            default: return 0f;
        }
    }
}
