using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System;

public enum MissionEnum
{
    [Description("OMG!! Something bad happens!")]
    [EventFatalness(Fatalness.Fatal)]
    Test_Event,

}

