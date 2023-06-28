
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailedScript : MonoBehaviour
{
    // Getting UI elements
    [SerializeField] private TMP_Text celebrateText;
    [SerializeField] private TMP_Text scoreHolder;

    // Defining posssible messages
    private string[] celebrations = {"Amazing Effort!", "Don't worry, you've got this", "One step at a time",
    "Practice makes perfect"};

    void Start()
    {
        // Updating the UI using a random celebration message and the score
        celebrateText.text = celebrations[UnityEngine.Random.Range(0, 4)];
        scoreHolder.text = "Score: " + VarsHolder.score;
    }

    // Update is called once per frame
    void Update()
    {
        // Quit the application if escape is pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Return to menu
    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // To Leaderboard
    public void toLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    // Replay level
    public void replay()
    {
        if (VarsHolder.currentLevel == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }

        else
        {
            SceneManager.LoadScene("Level " + VarsHolder.currentLevel);
        }
    }
}
