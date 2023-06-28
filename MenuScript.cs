
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    private void Start()
    {
        // Resetting the scores and levels
        VarsHolder.score = 0;
        
        for (int i = 0; i < VarsHolder.levelScores.Length; i++)
        {
            VarsHolder.levelScores[i] = 0;
        }
    }

    // Quitting
    public void quit()
    {
        Application.Quit();
    }

    // Run the tutorial
    public void beginTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // Being playing
    public void beginLvls ()
    {
        SceneManager.LoadScene("Level 1");
        VarsHolder.currentLevel = 1;
    }

    // Going to the credits
    public void toCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    private void Update()
    {
        // Quitting if escape is pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
