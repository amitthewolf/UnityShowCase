using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public int ExpWorth;
    public int LevelReq;
    private GameObject QGiver;
    public Quest PreQuest;
    public string QName;
    [TextArea(10, 100)]
    public string QDesc;
    public string QObj;
    private bool Completed = false;
    private int ObjNum = 0;
    public int ObjCompleted;

    public void AddObj()
    {
        if(!Completed)
        {
            ObjNum++;
            if (ObjNum == ObjCompleted)
            {
                Completed = true;
                Destroy(QGiver);
            }
        }
    }

    public void SetQGiver(GameObject Qgiver)
    {
        QGiver = Qgiver;
    }

    public string GetProgress()
    {
        return ObjNum.ToString();
    }

    public bool GetCompleted()
    {
        return Completed;
    }

    public void ResetQuest()
    {
        ObjNum = 0;
        Completed = false;
    }

    public bool RequirementsMet()
    {
        if (PreQuest != null)
        {
            QuestManager QM = QuestManager.instance;
            if (QM.QuestsCompleted.ContainsKey(PreQuest.QName))
                return true;
            else
            {
                return false;
            }
        }
        else
            return true;
    }
}
