using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC2Script : NPCBase
{
    void Update()
    {
        if (!started)
            return;
        if(questSystem.checkIndex() == 0)
        {
            questSystem.getCurrentQuest().goalCompleted();
        }
        if (waitForNext && Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            waitForNext = false;
            index++;
            if (!talkTwo)
            {
                //Check if we are in the scope fo dialogues List
                if (index < dialogue.Count)
                {
                    //If so fetch the next dialogue
                    GetDialogue(index);
                }
                else
                {
                    EndDialogue();
                    questSystem.StartQuest();
                }
            }
            else
            {
                //Check if we are in the scope fo dialogues List
                if (index < dialogue2.Count)
                {
                    //If so fetch the next dialogue
                    GetDialogue(index);
                }
                else
                {
                    EndDialogue();
                    questSystem.getCurrentQuest().goalCompleted();
                    questSystem.StartQuest();
                }
            }
        }
    }
}
