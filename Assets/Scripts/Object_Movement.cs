using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Movement : MonoBehaviour
{
    //private float movSensitivity = 4f;
    //private float posX, posY;

    // Axis Movement Disabled: not needed and could be confusing

    void Update()
    {
        //posX = Input.GetAxis("Horizontal");
        //posY = Input.GetAxis("Vertical");

        // if(posX != 0) transform.position += transform.right * posX * Time.deltaTime * movSensitivity;
        // if(posY != 0) transform.position += transform.up * posY * Time.deltaTime * movSensitivity;
    
        // if(Input.GetKey(KeyCode.Q)) transform.position += transform.forward * -1 * Time.deltaTime * movSensitivity;
        // if(Input.GetKey(KeyCode.E)) transform.position += transform.forward * 1 * Time.deltaTime * movSensitivity;
        
        
        // if(Input.GetKeyDown(KeyCode.R)) {
        //     transform.position = Vector3.zero;
        // }

    }
}
