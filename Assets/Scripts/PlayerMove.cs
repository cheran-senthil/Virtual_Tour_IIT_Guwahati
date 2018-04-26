/*
 *<header>
 * Module Name : PlayerMove.cs
 * Date of creation : 12/4/2018
 * Modification History : 
 *  12/4/2018 : Created Module for player movement
 *  19/4/2018 : Documented Code
 * Synopsis : This script maps the relevant key presses on the keyboard to the 
 *            corresponding movement of the player in the game.
 * Functions :
 *      void Awake()
 *      void Update()
 * Global Variables accessed/modified : NONE.
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // private variables used
    CharacterController charControl; //It allows us to easily do movement constrained by collisions without having to deal with a rigidbody.
    private float SPEED = 10.0f; // Sets Walking speed of the player
    private float JUMPSPEED = 10.0f; // Sets jump speed of the player
    private float GRAVITY = 20.0f; // Sets Gravity value in the game 
    private Vector3 moveDirection = Vector3.zero; // vector assigning direction of player movement

    /// <summary>
    /// This function is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        charControl = GetComponent<CharacterController>(); //Fetch the character controller component of the player.
    }

    /// <summary>
    /// This function is called every frame.
    /// This function helps in moving the character in the required direction.
    /// </summary>
    void Update()
    {
        if (charControl.isGrounded) // if player is on the ground then
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //set the moving direction of the player
            moveDirection = transform.TransformDirection(moveDirection); //changes the direction of the player
            moveDirection *= SPEED; //get the movement vector for the player
            if (Input.GetButton("Jump")) // incase the spacebar is pressed, jump in the Y axis
                moveDirection.y = JUMPSPEED;
        }
        moveDirection.y -= GRAVITY * Time.deltaTime; //if the player is in air, the fall down with speed equal to GRAVITY. The direction won't be modified  if the player is in air
        charControl.Move(moveDirection * Time.deltaTime); //move the player in the specified direction.
    }
}
 /* END OF THE CLASS */
