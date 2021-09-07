using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotManager : MonoBehaviour
{
    private List<IHot> HotList;
    private List<float> HotLastDmgTicks;
    private List<float> HotDurations;
    private List<int> HotStacks;

    // Start is called before the first frame update
    void Start()
    {
        HotList = new List<IHot>();
        HotLastDmgTicks = new List<float>();
        HotDurations = new List<float>();
        HotStacks = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = HotList.Count - 1; i >= 0; i--)
        {
            if (HotLastDmgTicks[i] + 1f < Time.time)
            {
                print("stacks" + HotStacks[i]);
                HotLastDmgTicks[i] = Time.time;
                float Heal = HotList[i].GetHeal() * HotStacks[i];
                HotDurations[i] = HotDurations[i] - 1;
                print("Durations" + HotDurations[i]);
                GetComponent<PlayerManager>().Heal((float)Math.Ceiling(Heal));
            }
            //print("Durations" + DotDurations[i]);
            if (HotDurations[i] == 0)
            {
                IHot Removed = HotList[i];
                HotList[i].HotOver();
                HotList.RemoveAt(i);
                HotDurations.RemoveAt(i);
                HotStacks.RemoveAt(i);
                HotLastDmgTicks.RemoveAt(i);
            }
        }
    }

    public bool AddHot(IHot NewHot)
    {
        bool exists = false;
        print("AddDot Called");
        for (int i = 0; i < HotList.Count; i++)
        {
            if (NewHot.GetHotID() == HotList[i].GetHotID())
            {
                print("found");
                exists = true;
                if (HotStacks[i] < NewHot.GetMaxStacks())
                    HotStacks[i] = HotStacks[i] + 1;
                HotDurations[i] = NewHot.GetDuration();
            }
        }
        if (!exists)
        {
            HotList.Add(NewHot);
            HotDurations.Add(NewHot.GetDuration());
            HotLastDmgTicks.Add(0);
            HotStacks.Add(1);
        }
        return exists;
    }

    public int GetNumofHots(IHot NewHot)
    {
        return HotList.Count;
    }
}
