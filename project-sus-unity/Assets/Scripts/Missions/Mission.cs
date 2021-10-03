using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JC;
using System;

public abstract class Mission : MonoBehaviour
{
    public abstract int Id { get; }
    public abstract Fatalness Fatalness { get; }
    public abstract string Title { get; }
    public abstract string Desc { get; }
    public abstract string Link { get; }

    /// <summary>
    /// On player click the mission
    /// </summary>
    public virtual void ReadInfo()
    {
        Time.timeScale = 0;

        PromptBoxSettings pbs = null;
        pbs = new PromptBoxSettings{
            Title = Title,
            Content = Desc,
            ConfirmButtonText = "Roger",
            CancelButtonText = "Learn More",
            ConfirmCallback = ()=>OnReadInfoConfirm?.Invoke(),
            CancelCallback = ()=>OnReadInfoCancel?.Invoke(),
        };

        // hide cancel (More information) if no link
        if(string.IsNullOrEmpty(Link))
        {
            pbs.CancelButtonText = "";
        }

        OnReadInfoConfirm += ()=> {
            Time.timeScale = 1;
        };
        OnReadInfoCancel += ()=>{
            PromptBox.Create(pbs);
            Application.OpenURL(Link);
        };

        PromptBox.Create(pbs);
    }

    public event Action OnReadInfoConfirm;
    public event Action OnReadInfoCancel;

    public virtual void Done()
    {
        MissionManager.Instance.RemoveMission(this);
        Destroy(gameObject);
    }

    public virtual void Fail()
    {
        MissionManager.Instance.RemoveMission(this);
        Destroy(gameObject);
    }



    protected List<Player> AllPlayer()
    {
        return GameController.Instance.Players;
    }

    protected Player RandomPlayer()
    {
        return AllPlayer()[UnityEngine.Random.Range(0, GameController.Instance.Players.Count)];
    }

    protected Player RandomPlayer(params Character[] excludeCharas)
    {
        var l = new List<Player>(AllPlayer());
        foreach(var c in excludeCharas)
        {
            l.RemoveAll(a => a.Character == c);
        }
        return l[UnityEngine.Random.Range(0, l.Count)];
    }

    protected Player CurrentPlayer()
    {
        return AllPlayer()[GameController.Instance.ControllingPlayerIndex];
    }

    /// <summary>
    /// nothing to tell with real readinfo box! 
    /// (won't apply on... events)
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    protected void CreateInformativeBox(string title, string desc, string link, Action OnConfirmExtraWork = null)
    {
        Time.timeScale = 0;
        PromptBoxSettings pbs = null;
        pbs = new PromptBoxSettings{
            Title = title,
            Content = desc,
            ConfirmButtonText = "Roger",
            CancelButtonText = "Learn More...",
            ConfirmCallback = ()=>{
                OnConfirmExtraWork?.Invoke();
                Time.timeScale = 1;
            },
            CancelCallback = ()=>{
                PromptBox.Create(pbs);
                Application.OpenURL(link);
            },
        };

        PromptBox.Create(pbs);
    }
}