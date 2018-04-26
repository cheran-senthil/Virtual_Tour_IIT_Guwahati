/*
 *<header>
 * Module Name : PlayerMove.cs
 * Date of creation : 15/4/2018
 * Modification History : 
 *  15/4/2018 : Created Module for player movement
 *  19/4/2018 : Documented Code
 * Synopsis : This script is the main controller of the game and training section.
 *            If it is game, a (random)task will be given to a user which he will have to complete in specified amount of time.
 *            If it is training, then when the user goes near any room, the description of that room is displayed on the screen.
 * Functions :
 *      void Start()
 *      void Update()
 *      void DisplayDescriptionText()
 *      void CheckForExit()
 *      void CheckDistance()
 *      float[] ExtractFinalCoordinates(List<string> room_coordinates, int index)
 *      float[] ExtractCoordinates(List<string> initial_position)
 * Global Variables accessed/modified :
 *      List<string> Database.initialPos
 *      List<string> Database.roomList
 *      List<string> Database.roomCoordinates
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// imports required from System module
using System.Collections.Generic;


public class Visualizer : MonoBehaviour
{
    private float currentTime = 0.0f; //stores the current time
    private bool timeStarted = false;
    public Button mainMenuButton; //Dedicated button to exit to main menu
    public Text instructionsText; //A dedicated textbox showing name of destination
    public static bool game=true; //Boolean variable specifying whether user is playing game(-true) or is in training(-false) mode
    public Text descriptionText; //Textbox for displaying Objective and Room Description
    public Light spotlight; //A refrence to the directed lights in the map
    private float[] finalPos; //
    private Vector3 finalPosition; //destination vector of the player
    private float initTime = 0.0f; //stores the time of the first keypress
    //private bool startCheck = false;
    private static string destinationName;
    public Text timer; //Textbox for displaying time
    private GameObject[] rooms; //refrece to the rooms in the map

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    /// This function is for initialising the variables.
    /// It also populates the game database via calling the Database.Start() method
    /// It extracts the list of available initial coordinates, list of room names, and corresponding coordinates from the database
    /// It also randomly selects an initial and final set of coordinates.
    /// The player is positioned on the initial coordinates and is given the task of reaching the final coordinates
    /// </summary>
    void Start()
    {
        Database.Start(); //populates the question/answer database of the game
        List<string> initial_position = new List<string>(Database.initialPos); //gets a list of available initial coordinates from the database
        float[] pos = ExtractCoordinates(initial_position); //extract the x,y,z coordinates of a random point from the list initial_points
        timer.text = ""; //initialise the Timer textbox
        mainMenuButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene(0); }); //setting the onClick function for the main menu button
        List<string> names = new List<string>(Database.roomList); //get the list of room names from the database
        List<string> room_coordinates = new List<string>(Database.roomCoordinates); //get the list of room coordinates from the database
        int index = Random.Range(0, names.Count); //randomly select a room for the destination position
        finalPos = ExtractFinalCoordinates(room_coordinates, index); //extract the x,y,z coordinates of the destination
        destinationName = names[index]; //set the name of the destination
        if (game) //if game bool is on, i.e., if he playing game
        {
            transform.position = new Vector3(pos[0], pos[1], pos[2]); //place the player on random initial position calculated above
            instructionsText.text = "Reach " + destinationName + " in 60 seconds"; //set the Instruction Text box in game mode
            spotlight.transform.position = new Vector3(finalPos[0], finalPos[1], finalPos[2]); //place a spotlight on final position
        }
        else
        {
            instructionsText.text = "Go to Red Marker at entry point of department to quit."; //set the Instruction Text box in training mode
        }
        rooms = GameObject.FindGameObjectsWithTag("Pickup"); //store all game objects with tag Pickup
        Time.timeScale = 0; //stop the time 
    }

    /// <summary>
    /// This method is called once every frame.
    /// on the first keypress, the timer starts.
    /// Thereafter, it continuously updates the description textbox
    /// it also checks for exit conditions (player reaches final destination or time is up)
    /// </summary>
    void Update()
    {
        if (Input.anyKeyDown) //if any key is pressed
        {
            Time.timeScale = 1; //make time back to normal
            if (!timeStarted) //if is false
            {
                initTime = Time.time; //initiate time and make timestarted true
                timeStarted = true;
            }
            Destroy(instructionsText); //destroy instruction text
        }
        DisplayDescriptionText();
        CheckForExit();
    }

    /// <summary>
    /// updates the description textbox
    /// </summary>
    void DisplayDescriptionText()
    {
        if (game) //In case the user is playing the game
        {
            if (timeStarted) // when the timer of game is started, show task in the description textbox
            {
                descriptionText.text = "Reach " + destinationName + " in 90 seconds";
            }
            else //else make the text empty
            {
                descriptionText.text = "";
            }
        }
        else //if the user is in training mode, show the name and description of the nearest room in the textbox
        {
            CheckDistance();
        }
    }

    /// <summary>
    /// This checks for exit conditions (player reaches final destination or time is up)
    /// </summary>
    void CheckForExit()
    {
        if (game && timeStarted) // if game and time both are started then
        {
            currentTime = Time.time;
            float time_difference = currentTime - initTime; //store the difference of time
            timer.text = "Time : " + time_difference.ToString("0.00") + "s"; //display time in the text box
            if ((time_difference) > 60) //if time difference is greater than 60s, change the scene
            {
                DisplayScore.getTimeValues(time_difference); //send time values to Display score
                timeStarted = false; //assign time started as false
                SceneManager.LoadScene(4); //load display score scene
            }

        }
        float distance = Vector3.Distance(transform.position, spotlight.transform.position);
        if (distance <= 5.0f) //if user is near the final position
        {
            timeStarted = false;
            if (game) //if in game mode
            {
                DisplayScore.getTimeValues(currentTime - initTime);
                SceneManager.LoadScene(3); //load question answers scene
            }
            else
            {
                SceneManager.LoadScene(0); //if loses load main menu scene
            }
        }
    }

    /// <summary>
    /// This checks if the player is near any room with the Tag "pickup", and then display the information about the corresponding room
    /// </summary>
    void CheckDistance()
    {
        foreach (GameObject room in rooms) //traversing all game objects with tag pickup
        {
            float distance = Vector3.Distance(transform.position, room.transform.position);
            if (distance <= 10.0f) //if user distance is less than 10 units
            {
                descriptionText.text = Database.GetInfo(room.name); //display info about the room
                return;
            }
        }
        descriptionText.text = ""; //otherwise display nothing
    }

    /// <summary>
    /// Extract the x,y, and z coordinates of a random point from the list of available "initial coordinates"
    /// </summary>
    /// <param name="initial_position">list of allowed initial coordinates (separated with spaces)</param>
    /// <returns></returns>
    float[] ExtractCoordinates(List<string> initial_position) 
    {
        float[] pos = new float[3]; //create an array to store the coordinates
        string[] temp = initial_position[Random.Range(0, initial_position.Count)].Split(' '); //select a random entry from the list and seperate it into 3 coordinates
        pos[0] = float.Parse(temp[0]); //extract the X Coordinate
        pos[1] = float.Parse(temp[1]); //extract the Y Coordinate
        pos[2] = float.Parse(temp[2]); //extract the Z Coordinate
        return pos;
    }

    /// <summary>
    /// Extract the x,y, and z coordinates of the "final destination"
    /// </summary>
    /// <param name="room_coordinates">list of coordinates of all the rooms (separated by spaces)</param>
    /// <param name="index">rondomly chosen index for determining final position</param>
    /// <returns></returns>
    float[] ExtractFinalCoordinates(List<string> room_coordinates, int index) //extract the x,y, and z coordinates of the final destination
    {
        float[] pos = new float[3]; //create an array to store the coordinates
        string[] temp = room_coordinates[index].Split(' '); //splits the string containing the final coordinates into 3 parts
        pos[0] = float.Parse(temp[0]); //extract the X Coordinate
        pos[1] = float.Parse(temp[1]); //extract the Y Coordinate
        pos[2] = float.Parse(temp[2]); //extract the Z Coordinate
        return pos;
    }
}
/* END OF THE CLASS */
