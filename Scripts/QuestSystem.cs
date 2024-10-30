using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] protected List<Quest> quests;
    private int questIndex;
    private Quest currentQuest;
    public TextMeshProUGUI questText;

    public void StartQuest()
    {
        currentQuest = quests[questIndex];
        currentQuest.enabled = true;
        currentQuest.Questing();
    }

    public void EndQuest()
    {
        currentQuest.enabled = false;
        questIndex++;
    }

    public int checkIndex()
    {
        return questIndex;
    }

    public Quest getCurrentQuest()
    {
        return currentQuest;
    }
    
    void Start()
    {
        questIndex = 0;
    }
}
