using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private WindowManager WindowManager;
    private GameObject QuestShow;
    public GameObject Solomon;
    public Quest Quest;
    private bool PlayerInRange;


    void Start()
    {
        WindowManager = GameObject.FindGameObjectWithTag("GM").GetComponent<WindowManager>();
        Quest.SetQGiver(this.gameObject);
        PlayerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && QuestManager.instance.CheckIfOnQuest(Quest.QName))
        {
            WindowManager.ToggleInteract(true);
            if (PlayerManager.Level >= Quest.LevelReq && Input.GetKeyDown(KeyCode.F))
            {
                if (GetComponent<AudioSource>() != null)
                    GetComponent<AudioSource>().Play();
                Solomon.SetActive(true);
                Solomon.GetComponent<Solomon>().StartChasing();
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WindowManager.GetComponent<WindowManager>().ToggleInteract(false);
            WindowManager.GetComponent<WindowManager>().ToggleQuestOffer(false);
            PlayerInRange = false;
        }
    }
}