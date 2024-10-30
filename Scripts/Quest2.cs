using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : Quest
{
    private int progress;
    private bool questStarted;

    public override void Questing()
    {
        questStarted = true;
        progress = 0;
        goal = "Kill 3 Squirrelmen ";
    }

    private void FixedUpdate()
    {
        if (questStarted)
        {
            if (progress == 3)
            {
                questSystem.questText.SetText("Go back to Michael");
            }
            else
            {
                questSystem.questText.SetText(goal + progress.ToString() + "/3");
            }
        }
    }

    public void progessMade()
    {
        progress++;
    }
}
