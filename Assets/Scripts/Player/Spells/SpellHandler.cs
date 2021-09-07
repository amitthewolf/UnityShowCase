using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHandler : MonoBehaviour
{
    public List<ISpell> Spells;
    public GameObject Player;

    private void Start()
    {
        Spells = new List<ISpell>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Spells.Count > 0 && Spells[0].ConditionsMet())
        {
            Spells[0].Cast();
        }
        else if (Input.GetKeyDown(KeyCode.E) && Spells.Count > 1 && Spells[1].ConditionsMet())
        {
            Spells[1].Cast();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && Spells.Count > 2 && Spells[2].ConditionsMet())
        {
            Spells[2].Cast();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && Spells.Count > 3 && Spells[3].ConditionsMet())
        {
            Spells[3].Cast();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && Spells.Count > 4 && Spells[4].ConditionsMet())
        {
            Spells[4].Cast();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && Spells.Count > 5 && Spells[5].ConditionsMet())
        {
            Spells[5].Cast();
        }
    }

    internal static void ResetLunge()
    {
        throw new NotImplementedException();
    }

    public void tooltip0()
    {
        if (Spells.Count > 0)
            Spells[0].tooltip();
    }

    public void tooltip1()
    {
        if (Spells.Count > 1)
            Spells[1].tooltip();
    }

    public void tooltip2()
    {
        if (Spells.Count > 2)
            Spells[2].tooltip();
    }

    public void tooltip3()
    {
        if (Spells.Count > 3)
            Spells[3].tooltip();
    }

    public void tooltip4()
    {
        if (Spells.Count > 4)
            Spells[4].tooltip();
    }

    public void tooltip5()
    {
        if (Spells.Count > 5)
            Spells[5].tooltip();
    }

    public void Hidetooltip()
    {
        TalentTree.tt.HideTooltip();
    }

}
