using PadlockSystem;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    [SerializeField] private Transform launchZone;      //fält för att sätta en launchzone i inspektorn, tar dess positon
    [SerializeField] private Transform destinationZone;     ////fält för att sätta en destinationzone i inspektorn, tar dess positon
    
    [SerializeField] private bool isActive = true; //fält för att aktivera eller inaktivera teleportern
    [SerializeField] private PadlockController padlockController; //referens till padlockcontroller scriptet

    private Vector3 teleportDestination = new Vector3(0f, 0f, 0f);


    void Awake()
    {
        if (launchZone != null)
        {

            if (destinationZone != null)
            {
                teleportDestination = destinationZone.position - launchZone.position;
                //beräknar teleportDestination så det går att använda modulärt

                //Debug.Log("launchZone.position set to: " + launchZone.position);
                //Debug.Log("destinationZone.position set to: " + destinationZone.position);
                Debug.Log($"Destination  of {launchZone.name} set to: {teleportDestination}");
            }
        }
        else
        {
            Debug.LogError("Launch Zone is not assigned in the inspector.");
        }

        if (padlockController != null)
        {
            // Prenumerera på PadlockController's CorrectCode event
            padlockController.CorrectCode += ActivateCorrectCode;
            padlockController.WrongCode += ActivateWrongCode;
        }

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            other.GetComponentInChildren<CharacterController>().enabled = false;
            // Teleport the player to a new location, other = player som går in i triggerzone
            other.transform.position += teleportDestination;

            other.GetComponentInChildren<CharacterController>().enabled = true;
            Debug.Log("Player teleported to: " + other.transform.position);
        }
    }

    private void ActivateCorrectCode()
    {
        isActive = !isActive;
        Debug.Log("ActivateCorrectCode triggered, switching state of isActive");
    }

    private void ActivateWrongCode()
    {
        //isActive = !isActive;
        Debug.Log("Wrong code triggered. no switching");
    }

    private void OnDestroy()
    {
        if (padlockController != null)
        {
            // Avprenumerera från PadlockController's events för att undvika minnesläckor
            padlockController.CorrectCode -= ActivateCorrectCode;
            padlockController.WrongCode -= ActivateWrongCode;
        }
    }
}
