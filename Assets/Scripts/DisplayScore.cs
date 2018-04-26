/*
 *<header>
 * Module Name : MainMenuScript.cs
 * Date of creation : 15/4/2018
 * Modification History : 
 *  15/4/2018 : Created module
 *  18/4/2018 : Documented Code
 * Synopsis : Calculates the score and displays it at the end.
 * Functions :
 *      void Start ()
 *      public void LoadOtherScene()
 *      void CalculateScore()
 *      public static void getTimeValues(float time_difference)
 * Global Variables accessed/modified :
 *      int Database.correctAnswers
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayScore : MonoBehaviour
{
    private static float timeDifference;
    private float score; 
    public Text scoreDisplay; //Textbox for displaying score

    /// <summary>
    /// This function is for initialising the variables and displaying score on the screen. 
    /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    /// </summary>
    void Start ()
    {
        CalculateScore(); //Calculates score on the basis of time taken and correct answers
        scoreDisplay.text = "Your Final Score is " + score.ToString("0.00"); //displays the score on the screen
    }

    /// <summary>
    /// This function is mapped to the "exit to main menu" button and on click, it loads the main menu scene
    /// </summary>
    public void LoadOtherScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Get the time taken to reach the destination. 
    /// This function is called from the "Visualizer" module, when the user is in "game" mode
    /// </summary>
    /// <param name="time_difference"></param>
    public static void getTimeValues(float time_difference)
    {
        timeDifference = time_difference;
    }

    /// <summary>
    /// Calculates the Score on the basis of time taken and correct answers
    /// </summary>
    void CalculateScore()
    {
        if(timeDifference > 60) //if Time taken to reach the destination is greater than 60s, score = 0
        {
            score = 0.0f;
        }
        else if(timeDifference <= 10) //if Time taken to reach the destination is less than 10s, score = 50 + 10 * Correct Answers
        {
            score = 50.0f;
            score+= 10* Database.correctAnswers; 
        }
        else //if Time taken to reach the destination is between 10s and 60s, score = 60 - timeDifference + 10 * Correct Answers
        {
            score = 60 - timeDifference;
            score += 10 * Database.correctAnswers;
        }
    }
}
/* END OF THE CLASS */
