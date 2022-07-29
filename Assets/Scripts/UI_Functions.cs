using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Functions : MonoBehaviour
{
    // for scene management, it is assumed that all non-object scenes come first
    // minIndex is set to be the first object scene, every scene after must also be an object scene
    // 0     : Menu
    // 1 - 6 : 360 Scenes
    // 7+    : Object Scenes

    private int targetIndex;
    private static int maxIndex; // exclusive, total number of scenes in build settings
    private int minIndex = 7;    // inclusive, first buildIndex of object scenes
    private int ready = 0;

    public TMP_Dropdown myDropdown;
    //public TMP_Text descText;

    void Awake() {
        maxIndex = SceneManager.sceneCountInBuildSettings;
        myDropdown.value = SceneManager.GetActiveScene().buildIndex - minIndex;
        ready = 1; // prevents value change on load from triggering scene select

    }

    public void NextScene() {
        targetIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (targetIndex < maxIndex) SceneManager.LoadScene(targetIndex);
    
    }
    public void PrevScene() {
        targetIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (targetIndex >= minIndex) SceneManager.LoadScene(targetIndex);

    }

    public void SetSceneByName(String scene) {
        SceneManager.LoadScene(scene);
    }
    public void SetObjectSceneByIndex(int index) {
        if (ready == 0) return;
        SceneManager.LoadScene(index + minIndex); 
        // +minIndex accounts for scenes ordered before object scenes since the dropdown index starts at 0
    }
    public void SetToMenuScene() {
        SceneManager.LoadScene(0);
    }

    

}
