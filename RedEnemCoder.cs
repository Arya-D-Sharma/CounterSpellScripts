using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class RedEnemCoder : MonoBehaviour
{
    // Getting UI Elements
    [SerializeField] private TextAsset file;
    [SerializeField] public TMP_Text promptBox;

    // Variables for words
    private string[] words;
    ArrayList checkedWords = new ArrayList();

    // Variables for data
    public string shown = "";
    public string key = "";

    // Variables for movement
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Rigidbody2D spellBody;

    void Start()
    {
        // Splitting all the words in the list by a comma
        // This returns a list of words
        words = file.text.Split(",");

        // Add all the words with length less than or equal to 9
        foreach (string word in words)
        {
            if (word.Length <= 9)
            {
                checkedWords.Add(word);
            }
        }

        // Getting a random word and setting it to the key
        int wordInd = UnityEngine.Random.Range(0, checkedWords.Count);
        key = checkedWords[wordInd].ToString().ToUpper();
        
        // Randomize and display the puzzle
        shown = Randomize(key);
        promptBox.text = shown;
    }

    private void FixedUpdate()
    {
        // Fix the position of the spell box to follow the enemy
        spellBody.position = new Vector2(body.position.x, body.position.y + 0.5f);
    }

    // This method jumbles the word passed
    private string Randomize(string inString)
    {

        // Creating a new arraylist and populating it with the letters of the word
        ArrayList letters = new ArrayList();

        for (int i = 0; i < inString.Length; i++)
        {
            letters.Add(inString.Substring(i, 1));
        }

        // Defining an return variable
        string outstring = "";

        // Add a random letter from the letters to a string, then remove it from the list.
        for (int j = 0; j < inString.Length; j++)
        {
            int randInd = UnityEngine.Random.Range(0, letters.Count);
            outstring += letters[randInd];
            letters.RemoveAt(randInd);
        }
        // returning
        return outstring;
    }
}
