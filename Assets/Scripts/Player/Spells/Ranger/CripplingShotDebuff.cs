using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CripplingShotDebuff : MonoBehaviour, IDot
{

    public int MaxStacks;
    public float DmgPerStack;
    public int DotID = 1;
    public float MaxDuration;
    public float Sprite;
    public float Slow;


    public bool CausesSlow()
    {
        return true;
    }

    public void DotOver()
    {
    }

    public float GetDmg()
    {
        return 0f;
    }

    public int GetDotID()
    {
        return 3;
    }

    public int GetDotType()
    {
        return 0;
    }

    public float GetDuration()
    {
        return 3f;
    }

    public int GetMaxStacks()
    {
        return 1;
    }

    public Sprite GetSprite()
    {
        return null;
    }

    public float SlowAmount()
    {
        return Slow;
    }

}
