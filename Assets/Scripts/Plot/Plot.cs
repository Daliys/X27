using System.Collections;
using UnityEngine;
using XInputDotNetPure; // Required in C#
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Plot : PlotObject
{
    PlayerIndex playerIndex;
    public UI_Quest ui_Quest;
    public NPC_mom nps_mom;
    public GameObject character;
    public PlayableDirector playableDirector;

    void Start()
    {
        StartCoroutine(PlotTasks());
    }

    
    IEnumerator PlotTasks() {
        ui_Quest.SetQuestText("");
        yield return new WaitForSeconds(6f);

        // ringing mobile phone
        interactives[0].interactiveObj.SetActiveInteractive(this, true, interactives[0].idName);
        StartCoroutine(MobilePhoneRinging(0));
        yield return WaitForAnswerAction(0);

        interactives[1].interactiveObj.SetActiveInteractive(this, true, interactives[1].idName);
        yield return WaitForAnswerAction(1);

        // mom congragulations
        yield return new WaitForSecondsRealtime(8);
        nps_mom.ActivateAction(this, interactives[2].idName);
        yield return WaitForAnswerAction(2);
        PrepareToAnimation();
        playableDirector.Play();
        yield return WaitForCutScene();
        Game.isPause = false;

        nps_mom.ActivateAction(this, interactives[2].idName);

        // ringing mobile phone
        yield return new WaitForSecondsRealtime(18);
        interactives[3].interactiveObj.SetActiveInteractive(this, true, interactives[3].idName);
        StartCoroutine(MobilePhoneRinging(3));
        yield return WaitForAnswerAction(3);

        interactives[4].interactiveObj.SetActiveInteractive(this, true, interactives[4].idName);
        yield return WaitForAnswerAction(4);
        ui_Quest.SetQuestText("Встретится с Сашей на посадке");
        // exitFromDoor

        interactives[5].interactiveObj.SetActiveInteractive(this, true, interactives[5].idName);
        yield return WaitForAnswerAction(5);

        // loadScene
        FindObjectOfType<SceneLoader>().LoadScene(2);

        //SceneManager.LoadScene(1);

    }

    private void PrepareToAnimation()
    {
        Game.isPause = true;
        character.transform.LookAt(nps_mom.gameObject.transform);
        nps_mom.gameObject.transform.LookAt(character.transform);
        character.GetComponent<MovementSystem>().SetAnimationBlowing();

    }

    IEnumerator WaitForCutScene()
    {
        while (playableDirector.state != PlayState.Paused)
        {
            yield return new WaitForFixedUpdate();
        }
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
