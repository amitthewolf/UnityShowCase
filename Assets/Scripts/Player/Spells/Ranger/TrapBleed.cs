using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBleed : MonoBehaviour, IDot
{
    public int MaxStacks;
    public float DmgPerStack;
    public int DotID = 1;
    public float MaxDuration;
    public float Sprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int GetDotID()
    {
        return 2;
    }

    public int GetDotType()
    {
        return 1;
    }

    public float GetDuration()
    {
        return MaxDuration;
    }

    public int GetMaxStacks()
    {
        return MaxStacks;
    }

    public Sprite GetSprite()
    {
        throw new System.NotImplementedException();
    }

    public float GetDmg()
    {
        return (DmgPerStack * PlayerManager.Power / MaxDuration);
    }

    public void DotOver()
    {
    }

    public bool CausesSlow()
    {
        return false;
    }

    public float SlowAmount()
    {
        return 0;
    }
}