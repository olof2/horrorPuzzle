using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footStepSFX;

    private PlayerMovement movement;
    private CharacterController controller;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();

        StartCoroutine(PlayFootSteps());
    }

    IEnumerator PlayFootSteps()
    {
        while (true)
        {

            Vector3 horizontalVelocity = controller.velocity;
            horizontalVelocity.y = 0f;

            if (movement.enabled &&
                movement.canMove &&
                controller.isGrounded &&
                movement.currentInput.magnitude > 0.1f &&
                horizontalVelocity.magnitude > 0.15f &&
                footStepSFX.Length > 0 &&
                AudioManager.instance != null)
            {
                AudioClip clip = footStepSFX[Random.Range(0, footStepSFX.Length)];

                if (clip != null)
                {
                    float volume = Random.Range(0.18f, 0.23f);
                    AudioManager.instance.PlaySFX(clip, volume);

                }

                float stepDelay = 0.65f;

                /*Mathf.Lerp(
                    0.9f,
                    0.55f,
                    movement.currentInput.magnitude
                );*/

                yield return new WaitForSeconds(stepDelay);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
