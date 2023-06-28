using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EffectBehavior : MonoBehaviour
{

    // Getting an explosion effect
    [SerializeField] private GameObject explodeEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        // If a collision with a tile or enemy is detected, trigger explosion
        if (collision.gameObject.CompareTag("Tile") || collision.gameObject.CompareTag("Enem"))
        {
            // Create the effect
            GameObject effect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
            // Destroy the object, and destory the effect (delayed by 1 second)
            Destroy(gameObject);
            Destroy(effect, 1f);
        }
    }
}
