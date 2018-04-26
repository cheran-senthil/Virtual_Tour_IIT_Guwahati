/*
 *<header>
 * Module Name : PlayerMove.cs
 * Date of creation : 15/4/2018
 * Modification History : 
 *  15/4/2018 : Created Module for Question/Answers
 *  16/4/2018 : Added Randomisation to Questions and corresponding options
 *  19/4/2018 : Documented Code
 * Synopsis : This script assigns questions and answers that the user has to answer after reaching the destination.
 * Functions :
 *      void Start ()
 * 	    void GenerateRandomNumbers ()
 * 	    void AssignQuestions()
 *      public void LoadScene()
 *      int CheckAnswers()
 * Global Variables accessed/modified : 
 *      List<string> Database.questions
 *      List<string> Database.optionsA
 *      List<string> Database.optionsB
 *      int Database.correctAnswers
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// imports required from System module
using System.Collections.Generic;

public class QuestionAnswers : MonoBehaviour {

    private List<string> questions; //stores questions
    private List<string> optionsA; //stores correct option
    private List<string> optionsB; //stores incorrect option
    private List<int> randomNumbers; //stores random numbers
    private List<int> randomOptions; //stores random options
    public List<Text> questionsText; 
    public List<Toggle> toggle;

    /// <summary>
    /// This method initialises the list of question and answers with the halp of the database, and also prepares for randomisation of questions and answers.
    /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    /// </summary>
    void Start ()
    {
        Database.Start(); //populates the question/answer database of the game
        questions = new List<string>(Database.questions); //gets the questions from the database
        optionsA = new List<string>(Database.optionsA); //gets the set of correct options from the database
        optionsB = new List<string>(Database.optionsB); //gets the set of incorrect options from the database
        randomNumbers = new List<int>(); //for randomisation and ordering of questions
        randomOptions = new List<int>(); //for ordering of options for each question
        GenerateRandomNumbers(); //initialises the randomOptions and randomNumbers lists with random numbers
        AssignQuestions();
    }

    /// <summary>
    /// Initialises the randomOptions and randomNumbers lists with random numbers
    /// </summary>
    void GenerateRandomNumbers ()
    {
        HashSet<int> check = new HashSet<int>(); //Declares a hash table for O(1) access
        for (int i = 0; i < 5; i++) //for generating 5 random numbers
        {
            int cur_value = Random.Range(0, questions.Count); //initialises cur_value to a random integer between 0 and 5
            while (check.Contains(cur_value)) //if the number is already present in the hashtable, find a new one.
            {
                cur_value = Random.Range(0, questions.Count);
            }
            randomNumbers.Add(cur_value); //if cur_value is not already present in the hash table, then add it to the list
            randomOptions.Add(cur_value % 2); //it is to know correct option is to be assigned to option a or b.
            check.Add(cur_value); //now add cur_value to the hash table
        }
    }

    /// <summary>
    /// This method arranges the questions and corresponding options in the required order based on the randomNumbers list and randomOptions list
    /// </summary>
    void AssignQuestions() 
    {
        for(int i = 0; i < 5; i++) //iterates over to add 5 questions
        {
            questionsText[i].text = questions[randomNumbers[i]]; //sets the question
            if(randomOptions[i] == 0) //for randomisation of the options in each question
            {
                toggle[2 * i].GetComponentInChildren<Text>().text = optionsA[randomNumbers[i]]; //correct option is on even position if random option is 0
                toggle[2 * i + 1].GetComponentInChildren<Text>().text = optionsB[randomNumbers[i]]; 
            }
            else
            {
                toggle[2 * i].GetComponentInChildren<Text>().text = optionsB[randomNumbers[i]]; 
                toggle[2 * i + 1].GetComponentInChildren<Text>().text = optionsA[randomNumbers[i]]; //correct option is on odd position if random option is 1
            }
        }
    }

    /// <summary>
    /// This method sends the number of correct responses to the database and sends the user to the DisplayScore scene.
    /// This function is mapped to the "submit" button.
    /// </summary>
    public void LoadScene()
    {
        int correct_answers; //stores number of correct responses
        correct_answers = CheckAnswers(); //calculates the correct responses
        Database.correctAnswers = correct_answers; //sends this data to the database.
        SceneManager.LoadScene(4); //sends the user to the next scene
    }
    
    /// <summary>
    /// This method calculates the number of correct responses by the user.
    /// </summary>
    /// <returns>Number of correct answers</returns>
    int CheckAnswers()
    {
        int correct_answers = 0;
        for(int i = 0; i < 10; i++) //Iterating over each option to check if he chose it or not.
        {
            if (toggle[i].isOn) //if it was the option he chose then 
            {
                if(randomOptions[i/2] == 0 && (i % 2 == 0)) //if i is even and random option for question at i/2 position is even then 
                {
                    correct_answers++; //increment correct_answers counter
                }
                if(randomOptions[i/2] == 1 && (i % 2 == 1)) //if i is odd and random option for question at i/2 position is odd then 
                {
                    correct_answers++; //increment correct_answers counter
                }
            }
        }
        return correct_answers;
    }
}
 /* END OF CLASS */
