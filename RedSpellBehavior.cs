using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSpellBehavior : MonoBehaviour
{
    // Getting the body
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the object on a collision
        if (collision.gameObject.CompareTag("Tile"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {

    }
}
