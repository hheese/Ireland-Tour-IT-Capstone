using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Orbit : MonoBehaviour
{

    //The target object followed by the camera is usually an empty object
    public Transform target;
    private int mouseWheelSensitivity = 1; // wheel sensitivity setting
    private int mouseZoomMin = 1; // minimum camera distance
    private int mouseZoomMax = 20; // maximum camera distance

    //private float movespeed = 10; // when the camera follows the speed (when the middle key is used for translation), it works when the smooth mode is used, and the larger the speed is, the smoother the movement is

    private float xSpeed = 700.0f; // camera X-axis rotation speed when rotating the viewing angle
    private float ySpeed = 500.0f; // camera Y-axis rotation speed when rotating the viewing angle

    private int yMinLimit = -360;
    private int yMaxLimit = 360;

    private float x = 0.0F; // stores the Euler angle of the camera
    private float y = 0.0F; // stores the Euler angle of the camera

    private float distance = 5; // the distance between the camera and the target, because the z-axis of the camera always points to the target, that is, the distance in the z-axis direction of the camera
    private Vector3 targetOnScreenPosition; // the screen coordinate of the target. The third value is the z-axis distance
    private Quaternion storeRotation; // stores the quaternion of the camera's attitude
    private Vector3 CameraTargetPosition; // location of target
    private Vector3 initPosition; // used to store the starting position of translation during translation
    private Vector3 cameraX; // camera's X-axis direction vector
    private Vector3 cameraY; // camera's Y-axis direction vector
    private Vector3 cameraZ; // camera z-axis direction vector

    private Vector3 initScreenPos; // the screen coordinates of the mouse when the middle key is just pressed (the third value is useless)
    private Vector3 curScreenPos; // the screen coordinates of the current mouse (the third value is useless)

    void Start()
    {
        //Here is the initial camera angle and some other variables, X and y... It corresponds to the mouse X and mouse Y of getaxis below
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        CameraTargetPosition = target.position;
        //storeRotation = Quaternion.Euler(y + 60, x, 0);
        storeRotation = Quaternion.Euler(y + 10, x, 0);
        transform.rotation = storeRotation; // set camera posture
        Vector3 position = storeRotation * new Vector3(0.0F, 0.0F, -distance) + CameraTargetPosition; // quaternion represents a rotation. Multiplying quaternion by vector is equivalent to rotating vector to corresponding angle, and then adding the position of target object is camera position
        transform.position = storeRotation * new Vector3(0, 0, -distance) + CameraTargetPosition; // set camera position

        // Debug.Log("Camera x: "+transform.right);
        // Debug.Log("Camera y: "+transform.up);
        // Debug.Log("Camera z: "+transform.forward);

        // //-------------TEST-----------------
        // testScreenToWorldPoint();

    }

    void Update()
    {
        //Right mouse button rotation function
        if (Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            storeRotation = Quaternion.Euler(y + 10, x, 0);
            var position = storeRotation * new Vector3(0.0f, 0.0f, -distance) + CameraTargetPosition;

            transform.rotation = storeRotation;
            transform.position = position;
        }
        else if  (Input.GetAxis("Mouse ScrollWheel") != 0) // mouse wheel zoom function
        {
            //print("wheel");
            if (distance >= mouseZoomMin && distance <= mouseZoomMax)
            {
                distance -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity;
            }
            if (distance < mouseZoomMin)
            {
                distance = mouseZoomMin;
            }
            if (distance > mouseZoomMax)
            {
                distance = mouseZoomMax;
            }
            var rotation = transform.rotation;

            transform.position = storeRotation * new Vector3(0.0F, 0.0F, -distance) + CameraTargetPosition;
        }

        //Middle mouse button pan
        if (Input.GetMouseButtonDown(1))
        {
            cameraX = transform.right;
            cameraY = transform.up;
            cameraZ = transform.forward;

            initScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetOnScreenPosition.z);
            Debug.Log("downOnce");

            //Targetonscreenposition. Z is the normal distance from the target object to the camera xmidbuttondownposition plane
            targetOnScreenPosition = Camera.main.WorldToScreenPoint(CameraTargetPosition);
            initPosition = CameraTargetPosition;
        }

        if (Input.GetMouseButton(1))
        {
            curScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetOnScreenPosition.z);
            //The coefficient of 0.01 is to control the speed of translation, which should be flexibly selected according to the distance between the camera and the target object
            target.position = initPosition - 0.01f * ((curScreenPos.x - initScreenPos.x) * cameraX + (curScreenPos.y - initScreenPos.y) * cameraY);

            //Recalculate location
            Vector3 mPosition = storeRotation * new Vector3(0.0F, 0.0F, -distance) + target.position;
            transform.position = mPosition;

            //// using this will make the camera's translation smoother, but it may not move the camera to the proper position when you button up, resulting in short jitter when you rotate and zoom again
            //transform.position=Vector3.Lerp(transform.position,mPosition,Time.deltaTime*moveSpeed);

        }
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("upOnce");
            //At the end of translation, update the position of cameratargetposition, otherwise it will affect the zoom and rotation functions
            CameraTargetPosition = target.position;
        }

    }

    //Limit angle to min ~ max
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void testScreenToWorldPoint()
    {
        //The third coordinate refers to the distance in the direction of the camera Z axis
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(CameraTargetPosition);
        Debug.Log("ScreenPoint: " + screenPoint);

        // var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(1,1,10));
        // Debug.Log("worldPosition: "+worldPosition);
    }
}