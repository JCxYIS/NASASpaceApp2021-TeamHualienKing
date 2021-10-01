using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] TMP_Text _titleText;
    [SerializeField] GameObject _buttonPrefab;

    List<Button> buttons = new List<Button>();
    [SerializeField] List<CharacterSelectButton> SelectedCharacters = new List<CharacterSelectButton>();



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {

        for(int i = 0; i < Settings.TOTAL_CHARCTER_COUNT; i++)
        {
            var go =  Instantiate(_buttonPrefab.gameObject, _buttonPrefab.transform.parent).GetComponent<CharacterSelectButton>();
            // go.Icon = ...
            Character chara = ((Character)i);
            go.Name.text = chara.ToString();
            go.Desc.text = EnumHelper.GetEnumDescription(chara);
            go.Character = chara;
            go.Button.onClick.AddListener(()=>Select(go));
            buttons.Add(go.GetComponent<Button>());
        }

        _buttonPrefab.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _titleText.text = $"SELECT {Settings.PLAYER_COUNT} CHARACTERS <size=40>{SelectedCharacters.Count}/{Settings.PLAYER_COUNT}</size>";
    }


    public void Select(CharacterSelectButton character)
    {
        var isActive = SelectedCharacters.Exists(c => c == character);

        if(isActive)
        {
            SelectedCharacters.Remove(character);
        }
        else
        {
            if(SelectedCharacters.Count >= Settings.PLAYER_COUNT)
            {
                return;
            }
            SelectedCharacters.Add(character);
        }

        isActive = !isActive;
        character.Button.GetComponent<Image>().color = isActive ? new Color(1, 1, 0.3f) : Color.white;
    }
}