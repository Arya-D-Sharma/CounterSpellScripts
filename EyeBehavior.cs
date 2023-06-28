
using UnityEngine;

public class EyeBehavior : MonoBehaviour
{

    // Defining components required
    private Rigidbody2D body;
    [SerializeField] private Rigidbody2D follow;
    [SerializeField] private Rigidbody2D target;

    [SerializeField] private RedEnemBehavior enem;

    // Speed of a spell
    [SerializeField] private float spellVel = 20f;

    // Variable to store the position of the mouse
    private Vector2 targetPos;

    // Variables to keep track of attacking and cooldown
    [SerializeField] private float coolDown = 200f;
    private float counter;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireBall;

    private void Start()
    {
        // Setting up the body
        body = GetComponent<Rigidbody2D>();

        counter = coolDown;
    }

    private void Update()
    {
        // Updating the position of the mouse
        targetPos = target.position;
    }

    private void FixedUpdate()
    {

        // The aimDir ends up being a vector between the two positions
        Vector2 aimDir = targetPos - body.position;
        // The arctangent returns the angle, given the x and y components of the vector
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        // Setting the position to the angle
        body.rotation = angle;

        // Setting the arrow to follow the rigidbody
        body.position = follow.position + new Vector2(0f, 0.3f);

        // Casting a spell when the timer runs out
        if (counter <= 0)
        {
            counter = coolDown;
            enem.isCasting = true;
            shoot();
        }

        // Updating the timer
        counter--;

    }

    // Method to shoot a projectile
    public void shoot()
    {

        GameObject effect = Instantiate(fireBall, firePoint.position, firePoint.rotation);
        Rigidbody2D spellBody = effect.GetComponent<Rigidbody2D>();
        // Adding force to the object
        spellBody.AddForce(transform.right * spellVel, ForceMode2D.Impulse);
    }
}