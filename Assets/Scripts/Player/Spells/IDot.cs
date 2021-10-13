using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDot
{
    int GetMaxStacks();
    int GetDotID();
    float GetDuration();
    Sprite GetSprite();
    int GetDotType();
    void DotOver();
    float GetDmg();
    bool CausesSlow();
    float SlowAmount();
}