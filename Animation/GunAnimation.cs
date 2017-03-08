using UnityEngine;
using System.Collections;

public class GunAnimation : MonoBehaviour {

    PlayerAnimations controller;

    Animator animator;

    public AudioClip shot;

    void Awake()
    {
        controller = GameObject.Find("Character").GetComponent<PlayerAnimations>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.isMoving)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (controller.isShooting)
        {
            animator.SetBool("shooting", true);
        }
        else
        {
            animator.SetBool("shooting", false);
        }

        if(controller.isAiming)
        {
            animator.SetBool("aiming", true);
        }
        else
        {
            animator.SetBool("aiming", false);
        }

        if(controller.isRunning)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

    }
}
