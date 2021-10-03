using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Event3_Health : Mission
{
    public override int Id => 3;

    public override Fatalness Fatalness => Fatalness.Warning;

    public override string Title => $"{victim.name} is not comfortable!";

    public override string Desc => $"Please go to the <color=yellow>medical module</color> to get some help!";

    public override string Link => "";

    [ReadOnly] 
    public Player victim;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        victim = RandomPlayer(Character.Doctor);
        victim.AddHealth(-15);
    }

    IEnumerator Start()
    {
        while(true)
        {
            victim.AddHealth(-1);
            yield return new WaitForSeconds(0.5f);
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
        if(obj.Name == "Medical Module")
        {
            victim.UpdateInteractTimeMax(5);
            if(victim.TickInteractTime())
            {
                enabled = false;
                CreateInformativeBox(
                    title: $"{victim.name} feels better!",
                    desc:"On the shuttle, due to the distance from the Earth, the space center may not be able to immediately give astrinauts a hand. Astronauts need to have the ability to solve problems by themself.\n"+
                         "And it is also very important of having adequate and good food and medicine reserves.",
                    link:"https://www.nasa.gov/hrp/hazards-of-human-spaceflight-videos",
                    OnConfirmExtraWork: ()=>Done());
            }
        }
    }

    


    public override void Done()
    {
        victim.AddHealth(3);
        base.Done();
    }

    public override void Fail()
    {
        throw new System.NotImplementedException();
    }
}