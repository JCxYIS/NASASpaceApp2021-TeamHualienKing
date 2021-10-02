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
            ConfirmButtonText = "Roger.",
            CancelButtonText = "More information",
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
    }

    public abstract void Fail();
}