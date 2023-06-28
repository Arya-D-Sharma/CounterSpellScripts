using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPassedBehavior : MonoBehaviour
{
    // Getting UI elements
    [SerializeField] private TMP_Text celebrateText;
    [SerializeField] private TMP_Text scoreHolder;

    // Getting game elements
    [SerializeField] private GameObject coin;
    [SerializeField] private Transform coinPoint;

    // Defining the celebratory messages
    private string[] celebrations = {"Terrific!", "Awesome!", "Amazing!", "Well Done!"};

    void Start()
    {
        // Updating UI
        celebrateText.text = celebrations[UnityEngine.Random.Range(0, 4)];
        scoreHolder.text = "Score: " + VarsHolder.score;

        // Adding coins based on the player score
        for (float i = 0; i < VarsHolder.levelScores[VarsHolder.currentLevel]; i++)
        {
            Instantiate(coin, coinPoint.position, Quaternion.identity);
            // Moving the position each time a coin is instantiated so a new coin
            // occurs at a new location
            coinPoint.position = coinPoint.position + new Vector3(1.5f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Quitting if escape is pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Returning to the menu
    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // To leaderboard
    public void toLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    // Next level
    public void nextLevel()
    {
        VarsHolder.currentLevel += 1;

        if (VarsHolder.currentLevel == 4)
        {
            toLeaderboard();
        }

        else
        {
            SceneManager.LoadScene("Level " + VarsHolder.currentLevel);
        }
    }
}