using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMissionSimple : Mission
{
    public override int Id => 1002;

    public override Fatalness Fatalness => Fatalness.Warning;

    public override string Title => "Never gonna give u up";

    public override string Desc => "You listen to an inspiring song! HP + 15";

    public override string Link => "https://youtu.be/dQw4w9WgXcQ";



    public override void ReadInfo()
    {
        OnReadInfoConfirm += ()=>Done();
        base.ReadInfo();
    }

    public override void Done()
    {
        GameController.Instance.Players.ForEach(p => p.AddHealth(15));
        base.Done();
    }

    public override void Fail()
    {
        throw new System.NotImplementedException();
    }
}