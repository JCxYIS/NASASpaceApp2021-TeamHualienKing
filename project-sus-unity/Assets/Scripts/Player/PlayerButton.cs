using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.AddressableAssets;

public class PlayerButton : MonoBehaviour
{
    [Header("Bindings")]
    public Image Icon;
    public TMP_Text Name;
    public TMP_Text HpText;
    public Slider HpSlider;
    public Image ControlBadge;

    [Header("Var")]
    [ReadOnly]
    public Player Player;

    [ReadOnly]
    public int Index;

    

    public void Init(Player player, int index)
    {
        Player = player;
        Index = index;

        GetComponent<Button>().onClick.AddListener(()=>GameController.Instance.SwitchPlayer(index));
        
        Character chara = player.Character;
        Addressables.LoadAssetAsync<Sprite>("icon_"+chara.ToString()).Completed += res => {
            Icon.sprite = res.Result;
        };
        Name.text = chara.ToString();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        HpText.text = $"{Player.Hp}/{Player.HpMax}";
        HpSlider.value = Player.Hp/(float)Player.HpMax;

        if(GameController.Instance.ControllingPlayerIndex == Index)
            ControlBadge.color = Color.green;
        else
            ControlBadge.color = Color.white;

    }
}