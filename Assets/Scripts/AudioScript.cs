/*
 *<header>
 * Module Name : AudioScript.cs
 * Date of creation : 15/4/2018
 * Modification History : 
 *  15/4/2018 : Created Settings Module
 *  19/4/2018 : Documented Code
 * Synopsis : This module helps user to change the global music volume.
 * Functions :
 *      void Awake()
 *      void Start()
 *      void Update()
 * Global Variables accessed/modified : 
 *      float Database.volume
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// imports required from System module
using System.Collections;
using System.Collections.Generic;

public class AudioScript : MonoBehaviour
{
    static AudioScript instance = null; 
    public AudioSource musicSource;

    /*
     *<Summary>
     * This function is called when the script instance is being loaded.
     *</Summary>
    */
    void Awake()
    {
        if (instance != null) //to make sure that only one instance of script is active
        {
            Destroy(gameObject); //destroy duplicate instances
        }
        else
        {
            instance = this; //assign instance to the attached game object
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    /*
    *<Summary>
    * This function is for initialising the variables. 
    * Initializes volume of music source.
    *</Summary>
    */
    void Start()
    {
        musicSource.volume = Database.volume;
        musicSource.Play();
    }

    /*
     *<Summary>
     * This function is called every frame.
     * This function continously updates the volume of music source
     *</Summary>
    */
    void Update()
    {
        musicSource.volume = Database.volume;
    }
}
 /* END OF CLASS */ 
