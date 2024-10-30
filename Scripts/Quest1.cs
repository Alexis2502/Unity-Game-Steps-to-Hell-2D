using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : Quest
{
    public override void Questing()
    {
        goal = "Talk to Michael";
        questSystem.questText.SetText(goal);
    }
}
