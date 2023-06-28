using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Driver : MonoBehaviour
{

    // Run speed variable
    [SerializeField] private float runVel = 40f;

    // Getting the controller and animator
    public Controller controller;
    public Animator animator;

    [SerializeField] private SpellBoxBehavior caster;

    // Movement variables
    private float moveX = 0f;
    private bool jump = false;

    public int lives = 3;

    // Animation Variables
    public bool isCasting = false;
    public bool isSelfSpell = false;

    public bool reachedObj = false;

    // Update is called once per frame
    void Update()
    {
        // Making sure that the player is not currently casting a spell
        if (!isCasting)
        {
            // Storing the keyboard input (either -1, 0 or 1) and multiplying it by a factor
            moveX = Input.GetAxisRaw("Horizontal") * runVel;

            // If the jump button was pressed, allow the jump
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
        

        // Setting the animator variables
        animator.SetFloat("Vel", Mathf.Abs(moveX));

        // Variables derived from controller
        animator.SetBool("Jump", controller.inJump);
        animator.SetBool("Falling", controller.falling);

        // Variables set under the spell box behavior
        animator.SetBool("Casting", isCasting);
        animator.SetBool("isAttacking", !isSelfSpell);
    }

    void FixedUpdate()
    {
        // Moving the character and turning off the jump

        if (!isCasting)
        {
            controller.Move(moveX * Time.fixedDeltaTime, jump);
            jump = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with the enemy or projectile, decrease life
        if (collision.gameObject.CompareTag("Enem") || collision.gameObject.CompareTag("Fireball"))
        {
            lives -= 1;
            // End the spell the player is casting
            caster.endSpell();
        }

        // If the objective is reached
        if (collision.gameObject.CompareTag("Objective"))
        {
            reachedObj = true;
        }
    }
}