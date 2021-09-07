using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    private WindowManager WindowManager;
    private GameObject QuestShow;
    public GameObject QuestIcon;
    public Quest Quest;
    private bool PlayerInRange;


    // Start is called before the first frame update
    void Start()
    {
        WindowManager = GameObject.FindGameObjectWithTag("GM").GetComponent<WindowManager>();
        Quest.SetQGiver(this.gameObject);
        PlayerInRange = false;
        if(Quest.RequirementsMet())
            QuestIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Quest.RequirementsMet() && !QuestManager.instance.CheckIfOnQuest(Quest.QName))
            QuestIcon.SetActive(true);
        if (PlayerInRange && Quest.RequirementsMet() && !QuestManager.instance.CheckIfOnQuest(Quest.QName))
        {
            WindowManager.ToggleInteract(true);
            if (PlayerManager.Level >= Quest.LevelReq && Input.GetKeyDown(KeyCode.F))
            {
                if (GetComponent<AudioSource>() != null)
                    GetComponent<AudioSource>().Play();
                WindowManager.ToggleQuestOffer(true);
                QuestShow = GameObject.FindGameObjectWithTag("QuestOffer");
                QuestShow.GetComponent<QuestOffer>().LoadQuest(Quest, QuestIcon);
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
