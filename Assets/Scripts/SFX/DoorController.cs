using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] public AudioClip openSound;
    [SerializeField] public AudioClip closeSound;

    private AudioSource audioSource;
    private Door door;
    private bool lastState;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        door = GetComponent<Door>();

        lastState = door.isOpen;

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    void Update()
    {
        if(door.isOpen != lastState)
        {
            if(door.isOpen)
            {
                audioSource.PlayOneShot(openSound);
            }
            else
            {
                audioSource.PlayOneShot(closeSound);
            }
            lastState = door.isOpen;
        }
    }





}
