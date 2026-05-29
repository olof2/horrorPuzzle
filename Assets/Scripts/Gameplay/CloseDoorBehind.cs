using PadlockSystem;
using UnityEngine;

public class closedoorbehindtrigger : MonoBehaviour
{

    [SerializeField] private STDoorController doorToClose; // Reference to the door controller that will be closed
    
    
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
            }
                
               
        }
    }

}
