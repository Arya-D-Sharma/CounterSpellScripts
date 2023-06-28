
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBehavior : MonoBehaviour
{
    // Getting UI and player
    [SerializeField] private Driver player;
    [SerializeField] private TMP_Text lives;
    [SerializeField] private TMP_Text spellCastBox;

    // Arrays to keep track of enemies and scores
    private int[] enemsToKill = { 0, 1, 3, 6 };
    private int[] maxEfficiencies = { 100, 2, 6, 10 };

    // Keeping track of user statistics
    public int enemsKilled = 0;
    public int spellsCast = 0;

    private int levelScore;

    private void Start()
    {
        // Setting the level variable based on the current scene to ensure no glitches
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            VarsHolder.currentLevel = 0;
        }

        else if (SceneManager.GetActiveScene().name.Equals("Level 1"))
        {
            VarsHolder.currentLevel = 1;
        }

        else if (SceneManager.GetActiveScene().name.Equals("Level 2"))
        {
            VarsHolder.currentLevel = 2;
        }

        else if (SceneManager.GetActiveScene().name.Equals("Level 3"))
        {
            VarsHolder.currentLevel = 3;
        }

    }

    void Update()
    {
        // If the player has no more lives, he is forced to give up the level
        if (player.lives <= 0)
        {
            giveUp();
        }

        // If escape is pressed, exit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // If the playe reaches the objective and has killed the required number of enemies for
        // the level, let the player, pass the player to the next level
        if (player.reachedObj && enemsKilled >= enemsToKill[VarsHolder.currentLevel])
        {
            VarsHolder.levelScores[VarsHolder.currentLevel] = getLevelScore();
            updateScore();
            pass();
        }

        // Update the text
        lives.text = "Lives: " + player.lives;
        spellCastBox.text = "Spells Cast: " + spellsCast;
    }

    public void giveUp()
    {
        // Open the scene for the level fail
        SceneManager.LoadScene("LevelFailed");
    }

    public void pass()
    {
        // Open the scene for the level passed
        SceneManager.LoadScene("LevelPassed");
    }

    public int getLevelScore()
    {
        // Get the most efficient way to complete the level
        float levelMax = maxEfficiencies[VarsHolder.currentLevel];

        // These conditionals determine the score of the player, based on the levels cast
        if (VarsHolder.currentLevel == 0)
        {
            return 0;
        }

        else if ((float) spellsCast == levelMax)
        {
            return 5;
        }

        else if ((float) spellsCast <= 1.5 * levelMax)
        {
            return 4;
        }

        else if ((float)spellsCast <= 2 * levelMax)
        {
            return 3;
        }

        else if ((float)spellsCast <= 3 * levelMax)
        {
            return 2;
        }

        return 1;
    }

    private void updateScore()
    {
        // Reset the score and set it to hte sum of the level scores.
        VarsHolder.score = 0;

        for (int i = 0; i < VarsHolder.levelScores.Length; i++)
        {
            VarsHolder.score += VarsHolder.levelScores[i]; 
        }
    }
}
