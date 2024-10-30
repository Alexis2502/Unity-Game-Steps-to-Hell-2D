using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC1Script : NPCBase
{
    private void Update()
    {
        if (!started)
            return;

        if (waitForNext && Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            waitForNext = false;
            index++;

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
    }
}
