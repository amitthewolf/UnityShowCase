using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestOffer : MonoBehaviour
{
    public GameObject GameManager;
    private Quest quest;
    private GameObject QuestGiverIcon;
    public TMP_Text QDescription;
    public TMP_Text QObjective;
    private string Qname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadQuest(Quest quest, GameObject QuestIcon)
    {
        QuestGiverIcon = QuestIcon;
        this.quest = quest;
        QDescription.text = quest.QDesc;
        QObjective.text = quest.QObj;
        Qname = quest.QName;
    }

    public void QuestAccepted()
    {
        QuestManager.instance.NewQuest(quest);
        gameObject.SetActive(false);
        QuestGiverIcon.SetActive(false);
    }
}
