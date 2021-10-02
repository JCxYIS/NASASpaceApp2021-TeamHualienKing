using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using JC.Utility;
using DG.Tweening;

public class PromptBox : MonoBehaviour
{
    [SerializeField] Text _titleText; 
    [SerializeField] Text _contentText; 
    [SerializeField] Button _confirmButton;
    [SerializeField] Button _cancelButton;

    /// <summary>
    /// The settings of this Prompt Box 
    /// </summary>
    private PromptBoxSettings _settings;
    


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Display the prompt box
    /// </summary>
    /// <param name="settings"></param>
    public void Show(PromptBoxSettings settings)
    {
        _settings = settings;

        // Texts
        _titleText.text = settings.Title;
        _contentText.text = settings.Content;

        // Callbacks
        _confirmButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.AddListener(OnConfirmButtonClick);
        _cancelButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.AddListener(OnCancelButtonClick);

        // Button Texts
        Text confirmButtText = _confirmButton.transform.GetChild(0).GetComponent<Text>();
        confirmButtText.text = _settings.ConfirmButtonText;
        if(_settings.CancelButtonText == "")
        {
            _cancelButton.gameObject.SetActive(false);
        }
        else
        {
            Text cancelButtText = _cancelButton.transform.GetChild(0).GetComponent<Text>();
            cancelButtText.text = _settings.CancelButtonText;
        }
        
        // on
        gameObject.SetActive(true);
        Transform target = transform.GetChild(0);
        target.DOScale(Vector3.one, 0.39f).From(Vector3.zero).SetEase(Ease.OutBounce).SetUpdate(true);
    }  

    public void Hide()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }


    public void OnConfirmButtonClick()
    {
        _settings.ConfirmCallback?.Invoke();
        Hide();
    }

    public void OnCancelButtonClick()
    {
        _settings.CancelCallback?.Invoke();
        Hide();
    }

    /* -------------------------------------------------------------------------- */

    /// <summary>
    /// Create a prompt box
    /// </summary>
    /// <param name="settings">PromptBoxSettings</param>
    public static void Create(PromptBoxSettings settings)
    {
        GameObject g = ResourcesUtil.InstantiateFromResources("PromptBox UI");
        g.GetComponent<PromptBox>().Show(settings);
    }

    /// <summary>
    /// A message box with content and confirm btn
    /// </summary>
    /// <param name="message"></param>
    /// <param name="callback"></param>
    public static void CreateMessageBox(string message, UnityAction callback = null)
    {
        Create(new PromptBoxSettings{
            Content = message,
            ConfirmCallback = callback,
            CancelButtonText = ""
        });
    }
}
