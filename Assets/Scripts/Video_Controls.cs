using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Video_Controls : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))  {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
        }
    }
}
