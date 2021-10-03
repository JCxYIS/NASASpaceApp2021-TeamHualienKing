using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FacilityText : MonoBehaviour
{
    Facility fac;
    TMP_Text text;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        text.text = fac?.Name;
        fac = null;
    }

    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        text = GetComponent<TMP_Text>();
        foreach(Player p in GameController.Instance.Players)
        {
            p.OnTriggerFacilityStay += UpdateText;
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        foreach(Player p in GameController.Instance.Players)
        {
            p.OnTriggerFacilityStay -= UpdateText;
        }
    }

    void UpdateText(Player p, Facility facility)
    {
        if(p.IsControlling)
            fac = facility;
    }
}