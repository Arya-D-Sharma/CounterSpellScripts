using System.Collections.Generic;
using UnityEngine;

public class VarsHolder : MonoBehaviour
{
    // Keeping track of the score
    public static int score = 0;

    // Keeping track of the level
    public static int currentLevel = 0; // 0 is for tutorial

    // Keeping track of where the next name will be entered on the leaderboard
    public static int insertPoint = 0;

    // Keeping track of entered scores and names
    public static List<int> scores = new List<int>();
    public static List<string> names = new List<string>();

    // Keeping track of score each level
    public static int[] levelScores = {0, 0, 0, 0};
    
}
