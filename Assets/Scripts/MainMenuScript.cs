/*
 *<header>
 * Module Name : MainMenuScript.cs
 * Date of creation : 12/4/2018
 * Author : Inderpreet Singh Chera
 * Modification History : 
 *  12/4/2018 : Created Module to navigate through Main Menu
 *  18/4/2018 : Documented Code
 * Synopsis : This script is linked to all the buttons in the Main Menu of the game
 *            and loads different scenes depending upon which button is pressed.
 * Functions :
 *      public void LoadScene(int button_id)
 *      public void Quit()
 * Global Variables accessed/modified :
 *      bool Visualizer.game
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    /// <summary>
    /// This function is mapped to 4 different buttons and on click, it loads a specific scene depending upon which button is pressed.
    /// </summary>
    /// <param name="button_id">represents the button id on the scene</param>
    public void LoadScene(int button_id) //button_id is taken as argument
    {
        if (button_id == 0) //if button with id 0 is pressed load scene 2(Start New Game)
        {
            Visualizer.game = true; //True implies start new game.
            SceneManager.LoadScene(2);
        }
        if (button_id == 1) //if button with id 1 is pressed load scene 2(Start Training)
        {
            Visualizer.game = false; //False implies start training.
            SceneManager.LoadScene(2);
        }
        if (button_id == 2) //if button with id 2 is pressed load scene 1(Go to settings)
        {
            SceneManager.LoadScene(1);
        }
        if (button_id == 3) //if button with id 3 is pressed exit the game.
        {
            Quit();
        }
    }

    /// <summary>
    /// This function is mapped to "quit" button and on click, it quits the game.
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR //if playing on Unity Game Engine exit back to editor
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); //else quit the application.
#endif
    }
}
/* END OF CLASS */
