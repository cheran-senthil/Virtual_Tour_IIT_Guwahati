/*
 *<header>
 * Module Name : Database.cs
 * Synopsis : This module acts as a store for data required by all the components of game.
 * Functions :
 *      public static void Start()
 *      private static void SeparateNameAndCoordinates(List<string> temp)
 *      public static string GetInfo(string room_name)
 * Global Variables accessed/modified : NONE.
 *</header> 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{

    public static List<string> roomList;        //stores list of room names
    public static List<string> roomCoordinates; //stores list of Coordinates of rooms
    public static string[] descriptionText;     //Stores description text for each room
    public static List<string> initialPos;      //stores the list of allowed initial positions
    public static List<string> questions;       //stores the set of questions
    public static List<string> optionsA;        //stores correct option for each question
    public static List<string> optionsB;        //stores incorrect option for each question
    public static int correctAnswers;           //Stores Correct Answers
    public static float volume = 0.5f;          //Global Music Volume

   /*
    *<Summary>
    * This function is for initialising the variables. 
    * Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    * This method is called by the "Visiualizer.cs", and the "QuestionAnswers.cs" scripts
    * This method loads the names, coordinates, and description of the rooms, and the sets of questions and corresponding options from the respective textassets, 
    *</Summary>
    */
    public static void Start()
    {
        TextAsset room_list = Resources.Load<TextAsset>("Places"); //loads the "Places" TextAsset
        List<string> temp = new List<string>(room_list.text.Split('\n'));
        SeparateNameAndCoordinates(temp); //extract room names and coordinates from the "places" textasset
        TextAsset room_info = Resources.Load<TextAsset>("Details"); //loads the "details" TextAsset
        descriptionText = room_info.text.Split('\n'); //extract room details from the "detail" textasset
        TextAsset coordinates = Resources.Load<TextAsset>("Initial"); //loads the Initial Coordinates TextAsset
        initialPos = new List<string> (coordinates.text.Split('\n')); //initialises list of allowed initial coordinates
        TextAsset questions_tmp = Resources.Load<TextAsset>("Questions"); //loads the "Questions" TextAsset
        questions = new List<string>(questions_tmp.text.Split('\n'));
        TextAsset options_a = Resources.Load<TextAsset>("OptionsA"); //loads the "OptionA" TextAsset
        optionsA = new List<string>(options_a.text.Split('\n'));
        TextAsset options_b = Resources.Load<TextAsset>("OptionsB"); //loads the "OptionB" TextAsset
        optionsB = new List<string>(options_b.text.Split('\n'));
    }

    /*
    *<Summary>
    * extracts the name and coordinates of the roos from the "room_list" list
    * </Summary>
    */
    private static void SeparateNameAndCoordinates(List<string> temp)
    {
        int i = 0;
        roomList = new List<string>(); //stores the names of all the rooms
        roomCoordinates = new List<string>(); //stores the coordinates for each of the rooms
        foreach(string a in temp) //iterate over each room
        {
            if (i % 2 == 0)
            {
                roomList.Add(a); //add the room name to the roomList list
            }
            else
            {
                roomCoordinates.Add(a); //add the room coordinates to the roomCoordinates list
            }
            i++;
        }
    }

    /*
    *<Summary>
    * Returns the Room Description a particular room
    *</Summary>
    */
    public static string GetInfo(string room_name)
    {
        int i = 0;
        while (i < descriptionText.Length)
        {
            if (room_name == descriptionText[i].Trim())
            {
                break;
            }
            i++;
        }
        if (i < descriptionText.Length)
        {
            return descriptionText[i + 1];
        }
        else
        {
            return "";
        }
    }
}
/* END OF THE CLASS */
