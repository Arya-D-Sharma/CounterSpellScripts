using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemBehavior : MonoBehaviour
{
    // Setting up variables the enemy requires
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Transform firePoint;
    private Rigidbody2D body;

    [SerializeField] public Animator animator;
    [SerializeField] private RedEnemCoder encoder;
    [SerializeField] GameObject eye;

    [SerializeField] private LevelBehavior level;

    public bool isCasting = false;
    public bool hasDied = false;

    // Variable to keep track of the direction of the enemy
    private bool forward = false;

    // Start is called before the first frame update
    void Start()
    {
        // Setting up the rigid body
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // Flipping the enemy to always face the player
        if (player.position.x < body.position.x && !forward)
        {
            Flip();
        }

        if (player.position.x > body.position.x && forward)
        {
            Flip();
        }

        // Updating the animator
        animator.SetBool("Attack", isCasting);
        animator.SetBool("Death", hasDied);

        isCasting = false;
    }

    // This method flips the enemy
    private void Flip()
    {
        forward = !forward;
        transform.Rotate(0f, 180f, 0f);
    }

    // If the enemy collides with an object of the same name as the key
    // (the player cast the correct spell), then trigger enemy death
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals(encoder.key))
        {
            hasDied = true;
            level.enemsKilled += 1;
            Destroy(eye);
            Destroy(gameObject, 0.33f);
        }
    }


}
