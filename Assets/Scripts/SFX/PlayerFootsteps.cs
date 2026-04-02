using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footStepSFX;

    private PlayerMovement movement;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();

        StartCoroutine(PlayFootSteps()) ;

    }

    IEnumerator PlayFootSteps()
    {
        while (true)
        {
            if(movement.currentInput.magnitude > 0.1f)
            {
                if(AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySFX(footStepSFX, 0.3f);
                }
            }

            yield return new WaitForSeconds(0.35f);
          
        }
    }

  
}
