using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesBulwark : MonoBehaviour,IHot
{

    public int MaxStacks;
    public float HealPerStack;
    public int HotID = 1;
    public float MaxDuration;
    public Sprite Sprite;

    private void Start()
    {
        PlayerManager.AddDmgResistance(0.2f);
    }

    public float GetDuration()
    {
       return MaxDuration;
    }

    public float GetHeal()
    {
        return (HealPerStack*PlayerManager.Power/MaxDuration);
    }

    public int GetHotID()
    {
        return HotID;
    }

    public int GetMaxStacks()
    {
        return MaxStacks;
    }

    public Sprite GetSprite()
    {
        return Sprite;
    }

    public void HotOver()
    {
        PlayerManager.RemoveDmgResistance(0.2f);
    }
}
