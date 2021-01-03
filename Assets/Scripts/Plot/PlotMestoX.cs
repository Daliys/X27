using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Cinemachine;

public class PlotMestoX : PlotObject
{
    public UI_Quest ui_Quest;
    public NPC_Artur nPC_Artur;
    public GameObject ui_Question;
    public GameObject mainCharacter;
    public PlayableDirector playableDirector1;
    public PlayableDirector playableDirector2;
    public CinemachineVirtualCamera CVC;

    private void Start()
    {
        StartCoroutine(PlotTasks());
    }


    IEnumerator PlotTasks()
    {
        yield return null;
        ui_Quest.SetQuestText("");
        yield return new WaitForSecondsRealtime(3f);
        ui_Quest.SetQuestText("Подойти к месту где ты сейчас играешь и посмотри на сцену");
        interactives[0].interactiveObj.SetActiveInteractive(this, true, interactives[0].idName);
        yield return WaitForAnswerAction(0);
        ui_Quest.SetQuestText("");


        Game.isPause = true;
        playableDirector1.Play();
        StartCoroutine(TeleportationCharacter());
        nPC_Artur.ActivateAction(this, interactives[1].idName);
        CVC.Priority = 100;

        yield return WaitForCutScene(playableDirector1);
        yield return WaitForAnswerAction(1);
        print("After wainti");

        interactives[2].interactiveObj.SetActiveInteractive(this, true, interactives[2].idName);
        nPC_Artur.ActivateActionRing(this, interactives[3].idName);
        playableDirector2.Play();
        //nPC_Artur.ActivateAction(this, interactives[1].idName);
        // interactives[3].interactiveObj.SetActiveInteractive(this, true, interactives[3].idName);

        //  yield return WaitForAnswerAction(3);
        yield return new WaitForSecondsRealtime(15f);

        ui_Question.SetActive(true);
        //Time.timeScale = 0;


        //ui_Quest.SetQuestText("You win");
        // yield return new WaitForSecondsRealtime(6);


        //SceneManager.LoadScene(4);


    }

    IEnumerator TeleportationCharacter()
    {
        yield return new WaitForSeconds(1f);
        mainCharacter.transform.LookAt(nPC_Artur.transform);
        mainCharacter.transform.position = new Vector3(-40.137f,mainCharacter.transform.position.y, 22.503f);
        mainCharacter.GetComponent<Animator>().SetBool("isRanning", false);
    }
    IEnumerator WaitForCutScene(PlayableDirector playableDirector)
    {
        while (playableDirector.state != PlayState.Paused)
        {
            yield return new WaitForFixedUpdate();
        }
    }
}
