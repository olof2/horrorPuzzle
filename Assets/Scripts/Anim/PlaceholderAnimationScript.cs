using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlaceholderAnimationScript : MonoBehaviour                 //script för placeholder animation
{
    private Animator animator;

    public TriggerZonePlaceholder zone;                                 //asign en "zone" till animation i inspect

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && zone.PlayerInsideZone)       //animation kan bara hända om PlayerInsideZone bool = true
        {
            animator.SetTrigger("isFalling");
        }
    }
}
