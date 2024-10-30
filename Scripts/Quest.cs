using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Quest : MonoBehaviour
{
    protected string goal;
    [SerializeField] protected QuestSystem questSystem;

    public void goalCompleted()
    {
        questSystem.EndQuest();
    }

    public abstract void Questing();
}
