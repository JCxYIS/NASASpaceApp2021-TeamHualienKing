using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Event1_MuscleLoss : Mission
{
    public override int Id => 1;

    public override Fatalness Fatalness => Fatalness.Information;

    public override string Title => $"{victim.name} is suffer from decreasing muscle mass and bone density";

    public override string Desc => $"<color=yellow>{victim.name}'s</color> muscle mass and bone density are decreasing!\n"+
                                    "Please go to <color=yellow>Gymnasium Module</color> to do some exercise!";

    public override string Link => "https://www.nasa.gov/hrp/hazards-of-human-spaceflight-videos";

    [ReadOnly] 
    public Player victim;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        victim = RandomPlayer();
    }

    IEnumerator Start()
    {
        while(true)
        {
            victim.AddHealth(-1);
            yield return new WaitForSeconds(0.7f);
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        victim.OnTriggerFacilityStay += OnVictimTriggerFacilityStay;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        victim.OnTriggerFacilityStay -= OnVictimTriggerFacilityStay;
    }

    private void OnVictimTriggerFacilityStay(Facility obj)
    {
        if(obj.Name == "Gymnasium Module")
        {
            victim.UpdateInteractTimeMax(3);
            if(victim.TickInteractTime())
            {
                enabled = false;
                CreateInformativeBox(
                    title: "The situation has been improved!",
                    desc:"In microgravity environment, astronaut's muscle mass will decrease at a rate of 1% per month.\n"+
                        "Therefore, astronauts spend about two and a half hours a day exercising to maintain physical function.",
                    link:"https://www.nasa.gov/stem/feature/shannon-walker-a-day-in-the-life-on-the-international-space-station.html",
                    OnConfirmExtraWork: ()=>Done());
            }
        }
    }

    


    public override void Done()
    {
        victim.AddHealth(5);
        base.Done();
    }

    public override void Fail()
    {
        throw new System.NotImplementedException();
    }
}