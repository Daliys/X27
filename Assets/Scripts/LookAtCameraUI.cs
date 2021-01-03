using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraUI : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camera;
    void Start()    
    {
        camera = Camera.main.transform ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
