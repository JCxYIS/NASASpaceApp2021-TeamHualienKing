using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class LandingScene : MonoBehaviour
{
    [SerializeField] TMP_Text _ver;
    [SerializeField] TMP_Text _pressToStart;
    [SerializeField] TMP_Dropdown _Dropdown;
    Difficulty difficulty;


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

        int maxUnlock = PlayerPrefs.GetInt("MAX_DIFF", 0);
        print("max diff="+maxUnlock);
        _Dropdown.ClearOptions();
        _Dropdown.value = 0;
        if(maxUnlock == 0)
        {
            _Dropdown.gameObject.SetActive(false);
        }
        else
        {
            List<string> haha = new List<string>();
            Difficulty.HahaLol().ForEach(d =>{
                if(maxUnlock >= 0)
                    haha.Add(d.Title);
                if(maxUnlock < 6)
                    maxUnlock--;
            });
            _Dropdown.AddOptions(haha);
        }
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            Next();
        }
    }

    public void Next()
    {
        GameManager.Instance.LandingSceneOk(Difficulty.HahaLol()[_Dropdown.value]);
    }
}