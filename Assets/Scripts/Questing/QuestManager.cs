using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public static List<QuestTrio> Quests;
    public Hashtable QuestsCompleted;
    public GameObject QuestList;
    public GameObject QuestInLog;
    public GameObject QuestDesc;
    public GameObject QuestSummary;
    public GameObject QuestObj;
    public GameObject QuestProgress;
    public GameObject ActiveList;
    public GameObject ActiveQuest;
    public Quest TrackQuest;
    private void Awake()
    {
        instance = this;
        Quests = new List<QuestTrio>();
        QuestsCompleted = new Hashtable();
    }
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per framel
    void Update()
    {
        if(TrackQuest!=null)
        {
            QuestDesc.GetComponent<TMP_Text>().text = TrackQuest.QDesc;
            QuestSummary.GetComponent<TMP_Text>().text = TrackQuest.QObj;
            QuestObj.GetComponent<TMP_Text>().text = TrackQuest.ObjCompleted.ToString();
            QuestProgress.GetComponent<TMP_Text>().text = TrackQuest.GetProgress();
        }
    }

    public void NewQuest(Quest quest)
    {
        if (!CheckIfOnQuest(quest.QName))
        {
            TrackQuest = quest;
            quest.ResetQuest();
            GameObject NewQuest = Instantiate(QuestInLog);
            NewQuest.transform.SetParent(QuestList.transform);
            NewQuest.GetComponent<TMP_Text>().text = quest.QName + " (Level - " + quest.LevelReq + ")";
            NewQuest.transform.localScale = new Vector3(1, 1, 1);
            NewQuest.GetComponent<QuestInLog>().SetQuest(quest);
            GameObject NewActiveQuest = Instantiate(ActiveQuest);
            NewActiveQuest.transform.SetParent(ActiveList.transform);
            NewActiveQuest.transform.localScale = Vector3.one;
            NewActiveQuest.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = quest.QName + " (Level - " + quest.LevelReq + ")";
            NewActiveQuest.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = quest.QObj + " : " + quest.GetProgress() + "/" + quest.ObjCompleted;
            Quests.Add(new QuestTrio(quest, NewQuest.GetComponent<TMP_Text>(), NewActiveQuest.transform.GetChild(1).gameObject.GetComponent<TMP_Text>(), NewActiveQuest));
        }
    }

    public bool CheckIfOnQuest(string QName)
    {
        bool ToReturn = false;
        Quests.Where(x => x.Quest.QName.Equals(QName)).ToList().AsParallel().ForAll(x =>
        {
            if (x.Quest.QName.Equals(QName))
                ToReturn = true;
        });
        return ToReturn;
    }

    public void AddObjective(string QName)
    {
        Quests.Where(x => x.Quest.QName.Equals(QName) && !x.Quest.GetCompleted()).ToList().ForEach(x => 
        {
            x.Quest.AddObj();
            x.QuestInActiveList.text = x.Quest.QObj + " : " + x.Quest.GetProgress() + "/" + x.Quest.ObjCompleted;
            if (x.Quest.GetCompleted())
            {
                QuestsCompleted.Add(x.Quest.QName,true);
                x.QuestInList.color = new Color32(0, 255, 0, 255);
                PlayerManager.instance.GainExp(x.Quest.ExpWorth);
                Destroy(x.GOInActiveList);
            }
        });
    }

    public class QuestTrio
    {
        public Quest Quest { get; private set; }
        public TMP_Text QuestInList { get; private set; }
        public TMP_Text QuestInActiveList { get; private set; }
        public TMP_Text QuestNameInActiveList { get; private set; }
        public GameObject GOInActiveList { get; private set; }

        public QuestTrio(Quest quest, TMP_Text questInList, TMP_Text questInactiveList, GameObject GoInList)
        {
            this.Quest = quest;
            this.QuestInList = questInList;
            this.QuestInActiveList = questInactiveList;
            this.GOInActiveList = GoInList;
        }

    }
}
