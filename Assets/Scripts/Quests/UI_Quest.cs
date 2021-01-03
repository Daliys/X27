using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Quest : MonoBehaviour
{
    public Text questText;
    
    public void SetQuestText(string questTextStr)
    {
        questText.text = questTextStr;
    }

}
