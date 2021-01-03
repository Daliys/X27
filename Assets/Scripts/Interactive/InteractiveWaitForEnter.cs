using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveWaitForEnter : InteractiveObject
{
    // Start is called before the first frame update
    bool isActive;

    // Update is called once per frame
    void Update()
    {
        if (isActive && (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Return)))
        {
            plot.InteractWithObject(idName);
        }
    }

    public override void SetActiveInteractive(PlotObject plot, bool isActive, string id)
    {
        this.isActive = isActive;
        this.plot = plot;
        idName = id;
    }
}
