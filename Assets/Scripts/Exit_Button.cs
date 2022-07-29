using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Button : MonoBehaviour
{
    public void ExitApp() {
        Application.Quit(); // Quits application on build but not in editor
        UnityEditor.EditorApplication.isPlaying = false;    // Quits out when in editor
    }

    public void SetObjectSceneByIndex(int index) {
        SceneManager.LoadScene(index); 
    }
}
