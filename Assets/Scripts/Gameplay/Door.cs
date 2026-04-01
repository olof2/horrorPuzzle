using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class Door : MonoBehaviour, I_Interactable
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    private Quaternion closedRot;
    private Quaternion openRot;
    private Coroutine coroutine;

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
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
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRot;
    }
}
