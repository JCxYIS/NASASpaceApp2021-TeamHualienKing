using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JC;

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
        PromptBoxSettings pbs = new PromptBoxSettings{
            Title = Title,
            Content = Desc,
            ConfirmButtonText = "Roger.",
            CancelButtonText = "More information",
        };

        if(string.IsNullOrEmpty(Link))
        {
            pbs.CancelButtonText = "";
        }

        PromptBox.Create(pbs);
    }

    public virtual void Done()
    {
        MissionManager.Instance.RemoveMission(this);
    }

    public abstract void Fail();
}