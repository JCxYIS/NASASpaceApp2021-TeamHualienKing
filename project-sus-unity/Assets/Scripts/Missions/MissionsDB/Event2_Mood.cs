using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Event2_Mood : Mission
{
    public override int Id => 2;

    public override Fatalness Fatalness => Fatalness.Information;

    public override string Title => $"{victim.name} is in a Bad Mood";

    public override string Desc => $"<color=yellow>{victim.name}</color>'s mood is quite bad!\n"+
                                    "Consider go to <color=yellow>sleep module</color>, have a nice sleep";

    public override string Link => "";

    [ReadOnly] 
    public Player victim;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        victim = RandomPlayer(Character.Philosopher);
        victim.AddHealth(-15);
    }

    IEnumerator Start()
    {
        while(true)
        {
            victim.AddHealth(-3);
            yield return new WaitForSeconds(1.5f);
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

    private void OnVictimTriggerFacilityStay(Player player, Facility obj)
    {
        if(obj.Name == "Sleeping Module")
        {
            victim.UpdateInteractTimeMax(5);
            if(victim.TickInteractTime())
            {
                enabled = false;
                CreateInformativeBox(
                    title: $"{victim.name}'s mood has been improved!",
                    desc:"During space travel, being in a closed, small space for a long time may cause many adverse effects on the human body.\n"+
                         "In addition to bad mood, that may also reduce cognition, morale, and interpersonal interaction, those will serious affect teamwork and task progress.",
                    link:"https://www.nasa.gov/hrp/hazards-of-human-spaceflight-videos",
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