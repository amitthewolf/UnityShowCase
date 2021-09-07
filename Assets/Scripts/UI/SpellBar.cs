using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBar : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Spellslots;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for ( int i = 0; i < Player.GetComponent<SpellHandler>().Spells.Count; i ++)
        {
            Spellslots[i].transform.GetChild(1).GetComponent<Image>().fillAmount = Player.GetComponent<SpellHandler>().Spells[i].CooldownState();
            Spellslots[i].transform.GetChild(0).GetComponent<Image>().sprite = Player.GetComponent<SpellHandler>().Spells[i].GetSprite();
            Spellslots[i].transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Spellslots[i].transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 0, 0, 180);
        }
            
    }

}
