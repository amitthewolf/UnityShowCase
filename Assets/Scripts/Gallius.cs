using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gallius : MonoBehaviour
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
            if (QuestManager.instance.CheckIfOnQuest("Talk to the Knight"))
                QuestManager.instance.AddObjective("Talk to the Knight");
        }
    }
}
