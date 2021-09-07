using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    private List<IDot> DotList;
    private List<float> DotLastDmgTicks;
    private List<float> DotDurations;
    private List<int> DotStacks;
    private float OriginalSpeed;
    private List<GameObject> DotGO;

    // Start is called before the first frame update
    void Start()
    {
        DotList = new List<IDot>();
        DotLastDmgTicks = new List<float>();
        DotDurations = new List<float>();
        DotStacks = new List<int>();
        DotGO = new List<GameObject>();
        OriginalSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (OriginalSpeed == 0)
            if (GetComponent<NewEnemyAI>() != null)
                OriginalSpeed = GetComponent<NewEnemyAI>().GetOriginalSpeed();
            else if(GetComponent<Solomon>() != null)
                OriginalSpeed = GetComponent<Solomon>().GetOriginalSpeed();
        int index = 0;
        DotList.ToList().ForEach(x =>
        {

            if (DotLastDmgTicks[index] + 1f < Time.time)
            {
                DotLastDmgTicks[index] = Time.time;
                float DmgToDeal = DotList[index].GetDmg() * DotStacks[index];
                DotDurations[index] = DotDurations[index] - 1;
                if (DmgToDeal > 0)
                    GetComponent<EnemyHealth>().TakeDotDmg((float)Math.Ceiling(DmgToDeal), DotList[index].GetDotType());
            }
            if (DotDurations[index] == 0)
            {
                if (DotList[index].CausesSlow())
                    GetComponent<NewEnemyAI>().SpeedChange(-DotList[index].SlowAmount()*OriginalSpeed);
                IDot Removed = DotList[index];
                DotList.RemoveAt(index);
                DotDurations.RemoveAt(index);
                DotStacks.RemoveAt(index);
                DotLastDmgTicks.RemoveAt(index);
                Destroy(DotGO[index]);
                DotGO.RemoveAt(index);
            }
            index++;
        });
        //for (int i = DotList.Count-1; i >= 0; i--)
        //{
        //    if (DotLastDmgTicks[i] + 1f < Time.time)
        //    {
        //        DotLastDmgTicks[i] = Time.time;
        //        float DmgToDeal = DotList[i].GetDmg()*DotStacks[i];
        //        DotDurations[i] = DotDurations[i]-1;
        //        if (DmgToDeal > 0)
        //            GetComponent<EnemyHealth>().TakeDotDmg((float)Math.Ceiling(DmgToDeal), DotList[i].GetDotType());
        //    }
        //    //print("Durations" + DotDurations[i]);
        //    if (DotDurations[i] == 0)
        //    {
        //        if (DotList[i].CausesSlow())
        //            GetComponent<EnemyAI>().Slow(-DotList[i].SlowAmount());
        //        IDot Removed = DotList[i];
        //        DotList.RemoveAt(i);
        //        DotDurations.RemoveAt(i);
        //        DotStacks.RemoveAt(i);
        //        DotLastDmgTicks.RemoveAt(i);
        //    }
        //}
    }

    public bool AddDot(IDot NewDot)
    {
        bool exists = false;
        print("AddDot Called");
        for (int i = 0; i < DotList.Count; i++)
        {
            if(NewDot.GetDotID() == DotList[i].GetDotID())
            {
                exists = true;
                if(DotStacks[i]<NewDot.GetMaxStacks())
                    DotStacks[i] = DotStacks[i]+1;
                DotDurations[i] = NewDot.GetDuration();
                
            }
        }
        if(!exists)
        {
            if (NewDot.CausesSlow())
            {
                if (GetComponent<NewEnemyAI>()!= null)
                    GetComponent<NewEnemyAI>().SpeedChange(NewDot.SlowAmount() * OriginalSpeed);
                else
                {
                    GetComponent<Solomon>().SpeedChange(NewDot.SlowAmount() * OriginalSpeed);
                }
            }
            DotList.Add(NewDot);
            DotDurations.Add(NewDot.GetDuration());
            DotLastDmgTicks.Add(0);
            DotStacks.Add(1);
        }
        return exists;
    }

    public void saveDot(GameObject DotGameObject)
    {
        DotGO.Add(DotGameObject);
    }

    public int GetNumofDots(IDot NewDot)
    {
        return DotList.Count;
    }
}
