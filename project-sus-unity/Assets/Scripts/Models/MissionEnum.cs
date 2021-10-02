using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System;

public enum Mission
{
    [Description("OMG!! Something bad happens!")]
    [EventFatalness(Fatalness.Fatal)]
    Test_Event,

}

