using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlotStreet : PlotObject
{
    public UI_Quest ui_Quest;
    public NPC_Sasha npc_sasha;
    public MovementSystem character;
    public PlayableDirector playableDirector;
    public GameObject characterGift;
    private void Start()
    {
        StartCoroutine(PlotTasks());
    }


    IEnumerator PlotTasks()
    {
        yield return null;
        ui_Quest.SetQuestText("Встретится с Сашей на посадке");
        interactives[0].interactiveObj.SetActiveInteractive(this, true, interactives[0].idName);
        yield return WaitForAnswerAction(0);

        character.gameObject.transform.LookAt(npc_sasha.gameObject.transform);
        npc_sasha.gameObject.transform.LookAt(character.gameObject.transform);
        playableDirector.Play();
        yield return WaitForCutScene();
        characterGift.SetActive(true);
        npc_sasha.HideGift();
        character.SetGiftinHand();
        //  ui_Quest.SetQuestText(" Вернутся домой и немного почилить дома");
        ui_Quest.SetQuestText("");
        yield return new WaitForSecondsRealtime(6);

        ui_Quest.SetQuestText("Вернутся домой и немного почилить дома");
        interactives[1].interactiveObj.SetActiveInteractive(this, true, interactives[1].idName);
        yield return WaitForAnswerAction(1);

        FindObjectOfType<SceneLoader>().LoadScene(3);

    }

    IEnumerator WaitForCutScene()
    {
        while (playableDirector.state != PlayState.Paused)
        {
            yield return new WaitForFixedUpdate();
        }
    }

}
