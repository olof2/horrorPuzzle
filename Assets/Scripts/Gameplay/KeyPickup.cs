using System;
using UnityEngine;

public class KeyPickup : MonoBehaviour, I_Interactable
{
    [SerializeField] private string keyID;
    public static event Action<string> OnKeyPickup;
    public void Interact()
    {
        OnKeyPickup?.Invoke(keyID);
        Destroy(gameObject); 
    }

}
