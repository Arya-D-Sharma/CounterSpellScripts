
using TMPro;
using UnityEngine;

public class SpellBoxBehavior : MonoBehaviour
{
    // Getting the driver, text, and position the box will interact with
    [SerializeField] private Driver driver;
    [SerializeField] private TMP_Text spellBox;
    [SerializeField] private Transform follow;
    [SerializeField] private Casting caster;
    [SerializeField] private Controller controller;
    [SerializeField] private LevelBehavior level;

    // Setting up a rigidbody for our spellbox
    [SerializeField] private Rigidbody2D body;

    // Variable to store the typed information
    public string spell = "";

    // Variables to take care of keyboard input
    private int letters = 0; // the number of letters allowed
    private bool[] keyStates = new bool[26]; // Keeping track of all the positions the keys were in

    // Defining all the keys so they can be called through a for loop
    private KeyCode[] keys = {KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M,
    KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z};
    
    // Writing the alphabet corresponding to the keys
    private string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    // Defining all the possible number inputs
    private KeyCode[] numberIns = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    // Writing numbers corresponding to the possible number entries
    private int[] lengths = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    // Update is called once per frame
    private void Update()
    {
        //If the right or left shift is pressed, and we are not already casting ...
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !driver.isCasting)
        {
            // If left shift, it is an attacking spell
            if (Input.GetKey(KeyCode.LeftShift))
            {
                driver.isSelfSpell = false;
            }

            // If it is right shift, it is a self spell
            else
            {
                driver.isSelfSpell = true;
            }

            // Checking what number key was pressed
            for (int k = 0; k < numberIns.Length; k++)
            {
                // If we did get a number input, set that number as the length of the spell
                if (Input.GetKey(numberIns[k]))
                {
                    letters = lengths[k];
                    driver.isCasting = true;
                }
            }
        }

        // Setting the position 
        body.position = follow.position;
    }

    private void FixedUpdate()
    {
        // Cast, or lauch additional behavior
        cast();
    }

    private void cast()
    {
        // Only run if we are already casting
        if (driver.isCasting)
        {
            // For every key defined
            for (int j = 0; j < keys.Length; j++)
            {
                // If the key was previously pressed, but now isn't
                if (keyStates[j] && !Input.GetKey(keys[j]))
                {
                    // Add the corresponding letter to the box
                    spellBox.text += alphabet[j];
                }

                // Update the states of the keys
                keyStates[j] = Input.GetKey(keys[j]);
            }

            // Calling endCast
            endCast();

        }
    }

    // This method checks to end the condition and take appropriate action
    public void endCast()
    {
        // If we have run out of letters ...
        if (spellBox.text.Length >= letters)
        {
            // Stop casting, copy the current spell, then clear the box
            driver.isCasting = false;
            spell = spellBox.text;
            spellBox.text = "";
            level.spellsCast += 1;

            // If the spell was for an attack, launch the spell
            if (!driver.isSelfSpell)
            {
                caster.shoot(spell);
            }

            else
            {
                // If the self spell of leap is called, upgrade the players next jump
                if (spell.Equals("LEAP"))
                {
                    controller.sJump = 1.75f;
                }
            }
            spell = "";
        }
    }

    // Ending the spell
    public void endSpell()
    {
        driver.isCasting = false;
        spellBox.text = "";
    }
}
