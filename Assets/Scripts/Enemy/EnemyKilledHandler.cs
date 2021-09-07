using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledHandler : MonoBehaviour
{
    private void Awake()
    {
        EnemyGeneral.EnemyKilled += CheckforQuest;
        EnemyGeneral.EnemyKilled += AddExp;
    }

    private void AddExp(object sender, EnemyGeneral.EnemyKilledEventArgs args)
    {
        PlayerManager.instance.GainExp(args.ExpWorth);
    }

    private void CheckforQuest(object Sender, EnemyGeneral.EnemyKilledEventArgs args)
    {
        if (args.ForQuest)
        {
            QuestManager.instance.AddObjective(args.QuestName);
        };
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
