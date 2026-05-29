using PadlockSystem;
using UnityEngine;
using System.Collections;

public class closedoorbehindtrigger : MonoBehaviour
{

    [SerializeField] private STDoorController doorToClose; // Reference to the door controller that will be closed
    [SerializeField] private PadlockController padlockToReset; // Reference to the padlock controller that will be reset (optional)


    public void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (you can use tags or layers to identify the player)
        if (other.CompareTag("Player"))
        {
            // Call the method to close the door
            if (doorToClose != null) //if door is assigned
            {
                if (doorToClose.isOpen) //and if the door is open
                    doorToClose.OpenDoor();

                StartCoroutine(ResetDoorAfterDelay(1.5f));
            }
            if (padlockToReset != null) //if padlock is assigned
            {
                padlockToReset.Reset(); //reset the padlock to its initial state (locked)
            }
        }
    }

    private IEnumerator ResetDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        doorToClose.Reset();
    }

}
