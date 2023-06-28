using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Casting : MonoBehaviour
{

    // Defining components required
    private Rigidbody2D body;
    [SerializeField] private Rigidbody2D follow;
    [SerializeField] private Controller controller;
    public Transform firePoint;
    public GameObject fireBall;
    [SerializeField] private Camera cam;

    // Speed of a spell
    [SerializeField] private float spellVel = 20f;

    // Variable to store the position of the mouse
    private Vector2 mousePos;

    private void Start()
    {
        // Setting up the body
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Updating the position of the mouse
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {

        //Calculating the angle at which to fire

        // The aimDir ends up being a vector between the two positions
        Vector2 aimDir = mousePos - body.position;
        // The arctangent returns the angle, given the x and y components of the vector
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        // Setting the position to the angle
        body.rotation = angle;

        // Setting the arrow to follow the rigidbody
        body.position = follow.position + new Vector2(0f, 0.5f);

    }

    // Method to shoot a projectile
    public void shoot(string n)
    {
        // Instantiating the appropriate effect as the desired location and rotation
        GameObject effect = Instantiate(fireBall, firePoint.position, firePoint.rotation);
        Rigidbody2D spellBody = effect.GetComponent<Rigidbody2D>();
        // Adding force to the object
        spellBody.AddForce(transform.right * spellVel, ForceMode2D.Impulse);
        // Naming it (this is useful for enemy collisions)
        spellBody.name = n;

    }
}