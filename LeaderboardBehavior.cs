using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardBehavior : MonoBehaviour
{

    // Defining the textboxes and fields the leaderboard will access
    [SerializeField] private TMP_Text names;
    [SerializeField] private TMP_Text scoreHolder;
    [SerializeField] private TMP_Text scores;
    [SerializeField] private TMP_Text ranks;
    [SerializeField] private TMP_InputField field;

    // Variable to prevent more than one entry
    private bool enteredOnce = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Updating the text
        scoreHolder.text = "Score: " + VarsHolder.score + "";
        names.text = "";
        scores.text = "";
        ranks.text = "";

        // Adding the names stores, as well as scores and ranks to the leaderboard
        for (int i = 0; i < VarsHolder.names.Count; i++)
        {
            names.text += "" + VarsHolder.names[i] + "\n";
            scores.text += "" + VarsHolder.scores[i] + "\n";
            ranks.text += "" + (i + 1) + "\n";
        }

        // Quitting if escape is pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Loading the menu screen
    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // to add a name to the list
    public void addName ()
    {
        // Check if entry is valid
        if (field.text.Length > 0 && !enteredOnce)
        {
            // Check what place the name should go in
            updateInsert();
            // Insert the name and score in the proper position
            VarsHolder.names.Insert(VarsHolder.insertPoint, field.text);
            VarsHolder.scores.Insert(VarsHolder.insertPoint, VarsHolder.score);
            // Reset the score
            VarsHolder.score = 0;
            
            // Reset scores for all the levels
            for (int i = 0; i < VarsHolder.levelScores.Length; i++)
            {
                VarsHolder.levelScores[i] = 0;
            }

            // Set the field to readonly and update the variable
            field.readOnly = true;
            enteredOnce = true;
        }
    }

    private void updateInsert()
    {
        // Reset the score
        VarsHolder.score = 0;

        // Set the score to te addition of all the level scores
        for (int j = 0; j < VarsHolder.levelScores.Length; j++)
        {
            VarsHolder.score += VarsHolder.levelScores[j];
        }

        int i = 0;
        // Go through the current list until there are no more names to go through, or we find 
        // where the score is less than the one next.
        while (i < VarsHolder.scores.Count && VarsHolder.score < VarsHolder.scores[i])
        {
            i++;
        }

        // Update the point at which the name will enter
        VarsHolder.insertPoint = i;
    }
}
