using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostLevelBehavior : MonoBehaviour
{
    // Getting UI elements
    [SerializeField] private TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        // Setting up the player score
        score.text = "Score: " + VarsHolder.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
