using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerUiObject : MonoBehaviour
{
    [SerializeField] Image _progressImage;
    [SerializeField] TMP_Text _nameText;

    [ReadOnly]
    public Player Player;

    [ReadOnly]
    public RectTransform CanvasRect;

    RectTransform rectTransform;
    Camera cam;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        rectTransform.anchoredPosition = JC.Utility.UIPosition.WorldToCanvasPos(Player.transform.position, CanvasRect, cam);
        // _nameText.text = $"{Player.name} ({Player.Hp}/{Player.HpMax}";
        
        if(Player.InteractTimeMax != 0)
            _progressImage.fillAmount = Player.InteractTime / Player.InteractTimeMax;
        else
            _progressImage.fillAmount = 0;
    }
}