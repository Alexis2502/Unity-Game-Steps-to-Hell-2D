using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest5 : Quest
{
    public override void Questing()
    {
        goal = "Find the chest";
        questSystem.questText.SetText(goal);
    }
}
