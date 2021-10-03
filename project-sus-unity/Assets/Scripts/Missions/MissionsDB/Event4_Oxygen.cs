using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Event4_Oxygen : Mission
{
    public override int Id => 4;

    public override Fatalness Fatalness => Fatalness.Warning;

    public override string Title => $"Somthing went wrong..?";

    public override string Desc => $"Listen! That is the alert of changing filter element of carbon dioxide scrubber!\n"+
                                    "Please go to payload bay to get new filter element, then find the carbon dioxide scrubber to replace filter element as new one.";

    public override string Link => "https://www.nasa.gov/pdf/146558main_RecyclingEDA(final)%204_10_06.pdf";





    IEnumerator Start()
    {
        while(true)
        {
            AllPlayer().ForEach(p => p.AddHealth(-1));
            yield return new WaitForSeconds(0.8f);
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        
        AllPlayer().ForEach(p => p.OnTriggerFacilityStay += OnTriggerFacilityStay);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        AllPlayer().ForEach(p => p.OnTriggerFacilityStay -= OnTriggerFacilityStay);
    }

    private void OnTriggerFacilityStay(Facility obj)
    {
        if(obj.Name == "Carbon Dioxide Scrubber")
        {
            enabled = false;
            CreateInformativeBox(
                title: $"There's new fresh air again!",
                desc:"Excessive carbon dioxide concentration can cause fatigue, rapid heartbeat, and dizziness in mild; loss of consciousness, muscle tremor, and even death in the severe case.\n"+
                     "In Apollo 13 mission, there was a serious carbon dioxide scrubber problem.",
                link:"https://www.nasa.gov/mission_pages/apollo/missions/apollo13.html",
                OnConfirmExtraWork: ()=>Done());
        }
    }

    


    public override void Done()
    {
        base.Done();
    }

    public override void Fail()
    {
        throw new System.NotImplementedException();
    }
}