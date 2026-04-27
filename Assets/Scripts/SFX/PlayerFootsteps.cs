using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footStepSFX;

    private PlayerMovement movement;
    void Start()
    {
        movement = GetComponent<PlayerMovement>(); // Hämta referensen till PlayerMovement-komponenten på samma GameObject.

        StartCoroutine(PlayFootSteps()); 

    }

    IEnumerator PlayFootSteps()
    {
        // Denna coroutine kommer att fortsätta så länge spelet körs, och den kommer att spela fotstegsljudet när spelaren rör sig.
        while (true)
        {
            if (movement.currentInput.magnitude > 0.1f)
            {
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySFX(footStepSFX, 0.2f);
                }

                yield return new WaitForSeconds(0.45f);

            }
            // Om spelaren inte rör sig, vänta en kort stund innan nästa kontroll för att undvika att spamma ljudet när spelaren börjar röra sig igen.
            else
            {
                yield return null;

            }


        }
    }
}
