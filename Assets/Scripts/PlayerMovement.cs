using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls playerControls;
    float direction = 0; // Horizontal direction of players movement

    public float speed = 400; // Speed of the player's movement
    bool isFacingRight = true; // Direction that the player is facing

    public float jumpForce = 5;  // Force of the player's jump
    bool isGrounded; // True if the player is currently on the ground
    int numberOfJumps = 0; // Number of jumps the player has made to see if we can double jump
    public Transform groundCheck; // Point on the player's body that checks for ground
    public LayerMask groundLayer;  // Layer that the ground is on

    public Rigidbody2D playerRigidbody;
    public Animator animator;

    private void Awake()
    {
        // Set up player controls(new input system)
        playerControls = new PlayerControls();
        playerControls.Enable();

        //Set up Input Actions
        playerControls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        playerControls.Land.Jump.performed += ctx => Jump();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);   // Check if the player is on the ground
        animator.SetBool("isGrounded", isGrounded); // Set the "isGrounded" parameter of the animator so we can play jump animation
        playerRigidbody.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRigidbody.velocity.y);  // Move the player horizontally
        animator.SetFloat("Speed", Mathf.Abs(direction));   // Set the "Speed" parameter of the animator based on the player's movement speed so we can play the run anim

        // Flip the player's sprite if they change direction
        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
        {
            Flip();
        }
            
    }

    // Flip back player's sprite and direction
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            // If the player is on the ground, jump and reset the number of jumps
            numberOfJumps = 0;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
            numberOfJumps++;
        }
        else
        {
            if(numberOfJumps == 1)
            {
                // If the player is in the air and has only jumped once, jump and increment the number of jumps
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
                numberOfJumps++;
            }
        }
    }
}
