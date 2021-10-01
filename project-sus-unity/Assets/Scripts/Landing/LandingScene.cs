using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class LandingScene : MonoBehaviour
{
    [SerializeField] TMP_Text _ver;
    [SerializeField] TMP_Text _pressToStart;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Color color = _pressToStart.color;
        color.a = 0.5f;
        _pressToStart.DOColor(color, 0.4f).SetLoops(-1, LoopType.Yoyo);
        _ver.text = Settings.VESRION;
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.anyKeyDown)
        {
            GameManager.Instance.LandingSceneOk();
        }
    }
}