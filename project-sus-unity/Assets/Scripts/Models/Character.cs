using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public enum Character
{
    [Description("Provides medical help")]
    Doctor,

    [Description("Payload slot +3")]
    BusinessMan,

    [Description("Recreational")]
    Poet,

    [Description("Fixes Mechanic Failures")]
    Enigneer,
}