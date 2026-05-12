using UnityEngine;

public class InteractDEBUG : MonoBehaviour, I_Interactable
{
    public void Interact()
    {
        Debug.Log("Object interacted!");
    }

    [SerializeField]
    private Transform uiAnchor;
    public Transform UIAnchor
    {
        get { return uiAnchor; }
        set { uiAnchor = value; }
    }

}
