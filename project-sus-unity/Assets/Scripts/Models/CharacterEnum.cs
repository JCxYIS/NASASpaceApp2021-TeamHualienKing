using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public enum Character
{
    [Description("Provides Medical Care")]
    Doctor,

    [Description("Fixes Mechanic Failures")]
    Engineer,

    [Description("Know How to Fly Planes")]
    Pilot,

    [Description("Cogito, ergo sum")]
    Philosopher,

}