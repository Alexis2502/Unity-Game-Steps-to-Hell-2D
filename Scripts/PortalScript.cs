using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : NPCBase
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
                if (questSystem.checkIndex() == 3)
                {
                    questSystem.getCurrentQuest().goalCompleted();
                }
                SceneManager.LoadScene("Hell");
            }
        }
    }
}
