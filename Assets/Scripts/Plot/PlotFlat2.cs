using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;


public class PlotFlat2 : PlotObject
{
    PlayerIndex playerIndex;
    public UI_Quest ui_Quest;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlotTasks());
    }
    IEnumerator PlotTasks()
    {
        ui_Quest.SetQuestText("");
        yield return new WaitForSeconds(60f);

        // ringing mobile phone
        interactives[0].interactiveObj.SetActiveInteractive(this, true, interactives[0].idName);
        StartCoroutine(MobilePhoneRinging(0));
        yield return WaitForAnswerAction(0);
        
        interactives[1].interactiveObj.SetActiveInteractive(this, true, interactives[1].idName);
        yield return WaitForAnswerAction(1);


        ui_Quest.SetQuestText("Выйти к дороге");
        interactives[2].interactiveObj.SetActiveInteractive(this, true, interactives[2].idName);
        yield return WaitForAnswerAction(2);
        //SceneManager.LoadScene(3);
        FindObjectOfType<SceneLoader>().LoadScene(4);

    }

    IEnumerator MobilePhoneRinging(int id)
    {
        while (!interactives[id].isCompleted)
        {

            GamePad.SetVibration(playerIndex, 0f, 1f);
            yield return new WaitForSecondsRealtime(0.7f);
            GamePad.SetVibration(playerIndex, 0f, 0f);
            yield return new WaitForSecondsRealtime(0.3f);
            GamePad.SetVibration(playerIndex, 0.0f, 0.9f);
            yield return new WaitForSecondsRealtime(0.9f);
            GamePad.SetVibration(playerIndex, 0f, 0f);
            yield return new WaitForSecondsRealtime(3f);

        }

        GamePad.SetVibration(playerIndex, 0f, 0f);

    }

    private void OnDisable()
    {
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }

}
