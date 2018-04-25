/*
 *<header>
 * Module Name : PlayerMove.cs
 * Date of creation : 15/4/2018
 * Author : Shubhendu Patidar
 * Modification History : 
 *  15/4/2018 : Created Settings Module
 *  19/4/2018 : Documented Code
 * Synopsis : This module helps user to change the global music volume.
 * Functions :
 *      void Start()
 *      void Update()
 *      public void LoadScene()
 * Global Variables accessed/modified : 
 *      float Database.volume
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour {

    public Slider volume; //Slider for adjusting music volume
    public AudioSource musicSource; //refrence to the Audio Source

    /// <summary>
    /// Initializes slider to current music volume.
    /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    /// </summary>
    void Start () {
        volume.value = Database.volume;
    }

    /// <summary>
    /// It updates the music volume in the database to the updated slider value.
    /// This is called once every frame.
    /// </summary>
    void Update () {
        Database.volume = volume.value;
    }

    /// <summary>
    /// This function is mapped to a button and on click, it loads the main menu scene
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
/* END OF THE CLASS */
