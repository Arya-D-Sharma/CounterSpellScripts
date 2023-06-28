using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    [SerializeField] private float jumpF = 400f; // Force of the jump
    [Range(0, .3f)][SerializeField] private float smoothMove = .05f; // Movement smoothing
    [SerializeField] private bool controlInAir = true; // Allows player to move while jumping
    [SerializeField] private LayerMask groundToPlayer; // Passing to the player what is ground
    [SerializeField] private Transform floor; // Check for floor
    [SerializeField] private Transform ceiling; // Check for ceiling

    public float vel = 10f; // Velocity of the player

    private bool grounded; // Check if the player is on the ground

    private float gColRad = 0.2f;

    private Rigidbody2D body; // The rigidbody of the player
    public bool forward = true;  // Determine the derection of the player
    private Vector3 v3Vel = Vector3.zero; // Freezing motion in the third dimension

    private float prevY;
    public bool falling;
    public bool inJump;

    public float sJump = 1f;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent; // Event the the player lands on the ground

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    // This function calls when the player is awaken
    private void Awake()
    {
        // Settting the variable to the Rigidbody
        body = GetComponent<Rigidbody2D>();

        // Setting up the Land event as a new Unity Event.
        // This is necessary because the first time it is called, its value is null
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }

        prevY = 0;
        falling = false;
        inJump = false;
    }

    // This will call a fixed number of times per second
    private void FixedUpdate()
    {
        // Setting up a variable to keep track of the original state of the character, before it changes
        bool wasGrounded = grounded;
        grounded = false;

        // Updating the grounded

        // Getting the colliders and checking
        Collider2D[] colliders = Physics2D.OverlapCircleAll(floor.position, gColRad, groundToPlayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }

        // If the position of the body has decreased by more than 0.05, it is falling
        if (prevY - body.position.y > 0.05)
        {
            falling = true;
        }

        //Otherwise, it is not falling
        else
        {
            falling = false;
        }

        // Resetting the previous position to current position
        prevY = body.position.y;

        // If the playe is not on the ground, and not falling, it is jumping
        if (!grounded && !falling)
        {
            inJump = true;
        }

        //Otherwise, it is not jumping
        else
        {
            inJump = false;
        }
    }


    public void Move(float move, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (grounded || controlInAir)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * vel, body.velocity.y);
            // And then smoothing it out and applying it to the character
            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref v3Vel, smoothMove);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !forward)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && forward)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump and is currently on the ground ...
        if (grounded && jump)
        {
            // Add a vertical force to the player.
            grounded = false;
            body.AddForce(new Vector2(0f, jumpF * sJump));
            sJump = 1;
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        forward = !forward;

        transform.Rotate(0f, 180f, 0f);
    }
}