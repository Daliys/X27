using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyButtonRespawn : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    float time;
    // Update is called once per frame
    
    void LateUpdate()
    {
        if ((Input.GetButton("Back") && Input.GetAxis("LT") > 0.5f && Input.GetButton("LB")) || Input.GetKey(KeyCode.B))
        {

            if (Time.time - 3 > time)
            {
                player.transform.position = transform.position;
            }
    
        }
        else
        {
            time = Time.time;
        }
        
    }


}
