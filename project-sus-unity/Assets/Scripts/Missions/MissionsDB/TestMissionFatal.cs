using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMissionFatal : Mission
{
    public override int Id => 1004;

    public override Fatalness Fatalness => Fatalness.Fatal;

    public override string Title => "Someone is VERY hurt";

    public override string Desc => $"AAAAAAA!! <color=yellow>{victim.name}</color> has stepped on LEGO!!! \nHis HP will harshly decrease until u find a doctor!";

    public override string Link => "https://youtu.be/dQw4w9WgXcQ";

    [ReadOnly, SerializeField]
    Player victim;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        victim = RandomPlayer();
        OnReadInfoConfirm += ()=>{
            StartCoroutine(Task());
        };
    }

    public override void ReadInfo()
    {
        base.ReadInfo();
    }

    IEnumerator Task()
    {
        for(int i = 0; true ; i++)
        {
            victim.AddHealth(-1);
            yield return new WaitForFixedUpdate();
        }
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