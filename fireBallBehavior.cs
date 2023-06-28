using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If a collision with a tile or enemy is detected, trigger explosion
        if (collision.gameObject.CompareTag("Tile") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
