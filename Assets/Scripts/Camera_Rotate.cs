using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_Rotate : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 500.0f;
    [SerializeField] private float clampAngle = 80.0f;    

    private Vector3 currentRotation;

    void Start()
    {
        currentRotation = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        currentRotation.y += mouseX * mouseSensitivity * Time.deltaTime;
        currentRotation.x += mouseY * mouseSensitivity * Time.deltaTime;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0.0f);
        transform.rotation = localRotation;


        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }

    
}
