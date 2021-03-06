using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMission : Mission
{
    public override int Id => 1001;

    public override Fatalness Fatalness => Fatalness.Information;

    public override string Title => "Test Mission OK";

    public override string Desc => "Click OK to ok.";

    public override string Link => "Link here";



    public override void ReadInfo()
    {
        Time.timeScale = 0;

        PromptBoxSettings pbs = new PromptBoxSettings{
            Title = Title,
            Content = Desc,
            ConfirmButtonText = "OK",
            CancelButtonText = "NoK",
            ConfirmCallback = ()=>{Done(); Time.timeScale = 1;},
            CancelCallback = ()=>{Fail(); Time.timeScale = 1;},
        };

        PromptBox.Create(pbs);
    }

    // public override void Done()
    // {
        
    // }

    public override void Fail()
    {
        foreach(var p in GameController.Instance.Players)
        {
            p.AddHealth(-50);
        }
        base.Fail();
    }
}