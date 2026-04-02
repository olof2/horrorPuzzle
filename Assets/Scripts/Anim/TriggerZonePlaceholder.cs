using UnityEngine;

public class TriggerZonePlaceholder : MonoBehaviour     //placeholder script för "trigger zones" där animation KAN triggas (animationer kan INTE triggas utanför zone)
{
    public bool PlayerInsideZone = false;

    private void OnTriggerEnter(Collider other)     //använder "isTrigger" med empty GameObject som "trigger zone"
    {
        Debug.Log(other.name + " entered the zone");    //debug, other=player " entered the zone"

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Zone");           //debug, visar att trigger registerar att playern har entered zonen
            PlayerInsideZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInsideZone = false;
        }
    }
}
