using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveMobilePhone : InteractiveObject
{
    public GameObject uiPanel;
    void Start()
    {
        
    }

    public override void SetActiveInteractive(PlotObject plot, bool isActive, string id)
    {
        uiPanel.SetActive(true);
        Game.isPause = true;
        this.plot = plot;
        Time.timeScale = 0;
        idName = id;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Back") || Input.GetButtonDown("Activation"))
        {
            uiPanel.SetActive(false);
            Game.isPause = false;
            plot.InteractWithObject(idName);
            Time.timeScale = 1;
        }
    }
}
