using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FiresViewCamera : MonoBehaviour
{
    public float mouseSensitivity = 1f;

    public Transform Player;
    float xRotation = 0f;
    float dist;
    Transform positionCamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        dist = Vector3.Distance(Player.position, transform.position);
        positionCamera = transform;
    }

    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;//* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;//* Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);

        if(Vector3.Distance(Player.position, transform.position) != dist)
        {
           // transform.DOMove(Player.position, 1f, false);
        }
    }
}
