using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    public TMP_Text Level;
    public TMP_Text Damage;
    public TMP_Text Experience;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Level.text = PlayerManager.Level.ToString();
        //Damage.text = PlayerManager.Power.ToString();
        //Experience.text = PlayerManager.Exp.ToString();
    }
}
