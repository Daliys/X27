using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveActivationObject : InteractiveObject
{
    private bool isInteractiveEnable;
    private bool isPossibleInteractivePanel;
    private GameObject canvasPressIcon;
    private GameObject canvasPointer;

    void Start()
    {
        canvasPressIcon = transform.GetChild(1).gameObject;
        canvasPointer = transform.GetChild(0).gameObject;

        isInteractiveEnable = false;

        // SetActiveInteractive(true);
    }

    private void Update()
    {
        if (isPossibleInteractivePanel && isInteractiveEnable)
        {
            if (Input.GetButtonDown("Activation"))
            {
                plot.InteractWithObject(idName);
                canvasPressIcon.SetActive(false);
                canvasPointer.SetActive(false);
                isInteractiveEnable = false;
            }
        }
    }


    public override void SetActiveInteractive(PlotObject plot, bool isActive, string id)
    {
        isInteractiveEnable = isActive;
        this.plot = plot;
        canvasPressIcon.SetActive(false);
        canvasPointer.SetActive(true);
        isPossibleInteractivePanel = false;
        idName = id;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (isInteractiveEnable)
            {
                canvasPressIcon.SetActive(true);
                canvasPointer.SetActive(false);
                isPossibleInteractivePanel = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isInteractiveEnable)
            {
                canvasPressIcon.SetActive(false);
                canvasPointer.SetActive(true);
                isPossibleInteractivePanel = false;
            }
        }
    }

}
