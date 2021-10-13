using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    // Start is called before the first frame update
    bool ConditionsMet();
    float GetCooldown();
    void SetLastUse();
    float LastUse();
    Sprite GetSprite();
    float CooldownState();
    int Subclass();
    void Cast();

    void tooltip();
}