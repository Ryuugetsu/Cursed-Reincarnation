using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraMoveSpeed = 120.0f;
    public GameObject cameraFollowObj;
    public float clampAngle = 80.0f;
    public float inputSensitivy = 150.0f;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    private float rotY = 0.0f;
    private float rotX = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        /*        
        Cursor.lockState = CursorLockMode.Locked; //trava o cursor do mouse no centro da tela
        Cursor.visible = false; //deixa o cursor do mouse invisivel;
        */
    }

    // Update is called once per frame
    void Update()
    {
        //We setup the rotation of the sticks here
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * inputSensitivy * Time.deltaTime;
        rotX += finalInputZ * inputSensitivy * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, +clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        //set the target to follow
        Transform target = cameraFollowObj.transform;

        //move towards the game object that is the target
        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
