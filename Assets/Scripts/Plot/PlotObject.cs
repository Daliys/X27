using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotObject : MonoBehaviour
{

    public Interactive[] interactives;


    [System.Serializable]
    public struct Interactive
    {
        public InteractiveObject interactiveObj;
        public string idName;
        public bool isCompleted;

        public Interactive(InteractiveObject obj, string name)
        {
            interactiveObj = obj;
            idName = name;
            isCompleted = false;
        }
    }

    protected IEnumerator WaitForAnswerAction(int id)
    {
        while (!interactives[id].isCompleted)
        {
            yield return new WaitForFixedUpdate();
        }
    }



    public void InteractWithObject(string id)
    {
        for (int i = 0; i < interactives.GetLength(0); i++)
        {
            if (interactives[i].idName == id)
            {
                interactives[i].isCompleted = true;
                return;
            }
        }
    }

}
