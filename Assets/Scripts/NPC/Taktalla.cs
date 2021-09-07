using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taktalla : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (QuestManager.instance.CheckIfOnQuest("Tak'talla the Grudge-bearer"))
                QuestManager.instance.AddObjective("Tak'talla the Grudge-bearer");
        }
    }
}
