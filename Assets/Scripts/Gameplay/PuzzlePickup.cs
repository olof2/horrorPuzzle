using Unity.VisualScripting;
using UnityEngine;

public class PuzzlePickup : MonoBehaviour, I_Interactable
{
    public Door doorToUnlock;
    public bool hasOpenedDoor = false;
    [SerializeField] private Transform uiAnchor;
    public Transform UIAnchor
    { 
        get { return uiAnchor; }
        set { uiAnchor = value; }
    }

    public void Interact()
    { 
        //behöver ej en interact actiom
    }

    public void OnPickedUp()
    {
        if (doorToUnlock != null && !hasOpenedDoor)
        {
            doorToUnlock.Unlock();
            hasOpenedDoor = true;
        } 
    }
}
