/*
 *<header>
 * Module Name : PlayerMove.cs
 * Synopsis : This script maps the relevant key presses on the keyboard to the 
 *            corresponding movement of the player in the game.
 * Functions :
 *      void Update()
 *      void RotateCamera()
 * Global Variables accessed/modified : NONE.
 *</header> 
*/

// imports required from UnityEngine module
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity;

    float xAxisClamp = 0.0f;

    /// <summary>
    /// This method is called once every frame.
    /// </summary>
    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        float rot_amount_x = mouse_x * mouseSensitivity;
        float rot_amount_y = mouse_y * mouseSensitivity;

        xAxisClamp -= rot_amount_y;

        Vector3 target_rot_cam = transform.rotation.eulerAngles;
        Vector3 target_rot_body = playerBody.rotation.eulerAngles;

        target_rot_cam.x -= rot_amount_y;
        target_rot_cam.z = 0;
        target_rot_body.y += rot_amount_x;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            target_rot_cam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            target_rot_cam.x = 270;
        }
        print(mouse_y);
        transform.rotation = Quaternion.Euler(target_rot_cam);
        playerBody.rotation = Quaternion.Euler(target_rot_body);
    }
}
/* END OF THE CLASS */
