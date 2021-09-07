using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestInLog : MonoBehaviour
{
    private Quest Quest;
    private QuestManager QM;

    public void SetQuest(Quest quest)
    {
        Quest = quest;
    }

    void Start()
    {
        QM = QuestManager.instance;
    }
    public void TrackQuest()
    {
        Debug.Log("Track Quest Clicked");
        QM.TrackQuest = Quest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
