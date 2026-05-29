using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, I_Interactable
{
    public float openAngle = 90f;
    public float openSpeed = 1f;
    public float speedMult = 6f;
    public bool isOpen = false;

    
    public bool isLocked = true;
    public bool openOnUnlock = true;
    

    private Quaternion closedRot;
    private Quaternion openRot;
    private Coroutine coroutine;

    public Door anotherDoorToOpen; //Används bara av dubbel dörrarna

    [SerializeField]
    private Transform uiAnchor;
    public Transform UIAnchor
    {
        get { return uiAnchor; }
        set { uiAnchor = value; }
    }

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        isLocked = false;
        
        Debug.Log("Door unlocked!");

        anotherDoorToOpen?.Unlock();

        if (openOnUnlock)
            Interact();
    }

    public void Interact()
    {
        if (isLocked)
        {
            Debug.Log("This door is locked");
            return;
        }

        if (coroutine != null)
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
