using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChoosingAnswer : MonoBehaviour
{
    public GameObject answerYes;
    public GameObject answerNo;

    void Start()
    {
        answerYes.SetActive(false);
        answerNo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Time.timeScale = 0;
        if (Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Horizontal") > 0)
        {
            answerNo.SetActive(true);
            answerYes.SetActive(false);
        }
        else
        {
            answerNo.SetActive(false);
            answerYes.SetActive(true);
        }
        if (Input.GetButtonDown("Activation"))
        {
            gameObject.SetActive(false);
            FindObjectOfType<SceneLoader>().LastScene();

        }
    }



}
