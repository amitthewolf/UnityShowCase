using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
public class Tooltip : MonoBehaviour
{
    public TMP_Text tooltipText;
    public TMP_Text tooltipTitle;
    public TMP_Text tooltipCost;
    public RectTransform backgroundRectTransform;


    public void ShowTooltip(string Title, string Cost, string Desc)
    {
        Vector3 Moveto = PlayerManager.MousePos;
        backgroundRectTransform.position = Moveto+new Vector3(2,0);
        tooltipTitle.text = Title;
        tooltipCost.text = Cost;
        tooltipText.text = Desc;
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
