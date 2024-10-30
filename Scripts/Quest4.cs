using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest4 : Quest
{
    public override void Questing()
    {
        goal = "Go to hell";
        questSystem.questText.SetText(goal);
    }
}
