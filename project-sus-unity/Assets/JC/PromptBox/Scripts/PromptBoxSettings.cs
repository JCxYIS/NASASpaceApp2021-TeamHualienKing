using UnityEngine.Events;

public class PromptBoxSettings
{
    public string Title = "";

    public string Content = "Are you sure?";

    public UnityAction ConfirmCallback = null;

    public UnityAction CancelCallback = null;    

    public string ConfirmButtonText = "確認";
    
    /// <summary>
    /// Set to "" (empty string) will hide the cancel button
    /// </summary>
    public string CancelButtonText = "取消";
}