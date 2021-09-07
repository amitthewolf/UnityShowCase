using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHot
{
    int GetMaxStacks();
    int GetHotID();
    float GetDuration();
    Sprite GetSprite();
    void HotOver();
    float GetHeal();
    
}