using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestMissionDanger : Mission
{
    public override int Id => 1003;

    public override Fatalness Fatalness => Fatalness.Danger;

    public override string Title => "Someone is hurt";

    public override string Desc => $"BONK! {victim.name} hit by a baseball bat! His HP -3 per seconds for 10 seconds!";

    public override string Link => "https://youtu.be/dQw4w9WgXcQ";

    [ReadOnly, SerializeField]
    Player victim;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        victim = GameController.Instance.Players[Random.Range(0, GameController.Instance.Players.Count)];
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
        for(int i = 0; i < 10 ; i++)
        {
            victim.AddHealth(-3);
            yield return new WaitForSeconds(1);
        }
        Done();
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