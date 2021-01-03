using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlotStreet2 : PlotObject
{
    public UI_Quest ui_Quest;

    private void Start()
    {
        StartCoroutine(PlotTasks());
    }


    IEnumerator PlotTasks()
    {
        yield return null;
        ui_Quest.SetQuestText("Выйти к дороге");
        interactives[0].interactiveObj.SetActiveInteractive(this, true, interactives[0].idName);
        yield return WaitForAnswerAction(0);
        ui_Quest.SetQuestText("");
        // yield return new WaitForSecondsRealtime(6);

        FindObjectOfType<SceneLoader>().LoadScene(5);
        //SceneManager.LoadScene(4);


    }

}
