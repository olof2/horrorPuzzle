using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class Door : MonoBehaviour, I_Interactable
{
    public float openAngle = 90f;
    public float openSpeed = 1f;
    public float speedMult = 6f;
    public bool isOpen = false;

    private Quaternion closedRot;
    private Quaternion openRot;
    private Coroutine coroutine;

    //lock system
    [SerializeField] private string requiredKeyId;
    public bool isLocked = false;

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    private void OnEnable()
    {
        KeyPickup.OnKeyPickup += TryUnlock;
    }

    private void OnDisable()
    {
        KeyPickup.OnKeyPickup -= TryUnlock;
    }

    private void TryUnlock(string keyId)
    {
        if (keyId == requiredKeyId)
        {
            isLocked = false;
        }
    }
    public void Interact()
    {
        if (isLocked) return;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ToggleDoor());
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRot = isOpen ? closedRot : openRot;
        isOpen = !isOpen;

        while(Quaternion.Angle(transform.rotation, targetRot) > 0.01f)
        {
            float speed = openSpeed;
            if(!isOpen) speed *= speedMult;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * speed);
            yield return null;
        }

        transform.rotation = targetRot;
    }
}
