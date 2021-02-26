using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimationController : MonoBehaviour
{
    Animator animator;
    CharController charController;
    void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        animator.SetBool("isSliding", charController.isSliding);
        animator.SetFloat("yVelocity", charController.velocityY);
        if (!charController.isGrounded) {
            animator.SetBool("isJumping", true);  
        } else {
            animator.SetBool("isRunning", Mathf.Abs(charController.movementX) > 0f);
            animator.SetBool("isJumping", false);
        }
    }
}
