using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlaceholderAnimationScript : MonoBehaviour, I_Interactable     //script för placeholder animation. När jag trycker "P" spelas animationen
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        animator.SetTrigger("isFalling");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("isFalling");
        }
    }

    //public void OnTrigger(InputValue input)
    //{
    //    if (animator != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
    //    {
    //        animator.SetTrigger("isFalling");
    //    }
    //}

}
