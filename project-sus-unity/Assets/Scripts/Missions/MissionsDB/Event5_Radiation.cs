using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Event5_Radiation : Mission
{
    public override int Id => 5;

    public override Fatalness Fatalness => Fatalness.Danger;

    public override string Title => $"High-intensity Space Radiation";

    public override string Desc => $"We are currently sailing smoothly and have left the Earth's magnetic field!\n"+
                                    "That means we are exposed to high-intensity space radiation!\n";

    public override string Link => "";





    IEnumerator Start()
    {
        for(int i = 5; i > 0; i-- )
        {
            AllPlayer().ForEach(p => p.AddHealth(-i*2));
            yield return new WaitForSeconds(1f);
        }
        Done();
    }
   
    


    public override void Done()
    {
        CreateInformativeBox(
            title: $"Radiation level is now normal",
            desc:"Being in a high radiation space for a long time may cause nausea, vomiting, fatigue and increase the risk of cancer, even have adverse effects for central nervous system, cognitive and motor function.",
            link:"https://www.nasa.gov/hrp/hazards-of-human-spaceflight-videos"
        );
        base.Done();
    }

    public override void Fail()
    {
        throw new System.NotImplementedException();
    }
}