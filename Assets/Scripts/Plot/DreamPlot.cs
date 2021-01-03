using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DreamPlot : PlotObject
{

    private void Start()
    {
        StartCoroutine(PlotTasks());
    }


    IEnumerator PlotTasks()
    {
        yield return null;
   
        interactives[0].interactiveObj.SetActiveInteractive(this, true, interactives[0].idName);
        yield return WaitForAnswerAction(0);
  
        // yield return new WaitForSecondsRealtime(6);

        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(4);


    }
}
