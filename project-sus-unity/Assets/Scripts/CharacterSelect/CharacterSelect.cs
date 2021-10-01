using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine.AddressableAssets;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] TMP_Text _titleText;
    [SerializeField] GameObject _buttonPrefab;
    [SerializeField] Button _startGameButton;

    List<Button> buttons = new List<Button>();
    [ReadOnly, SerializeField] List<CharacterSelectButton> selectedCharacters = new List<CharacterSelectButton>();



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _startGameButton.transform.DOScale(Vector3.one * 1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        _startGameButton.gameObject.SetActive(false);
        for(int i = 0; i < Settings.TOTAL_CHARCTER_COUNT; i++)
        {
            var go =  Instantiate(_buttonPrefab.gameObject, _buttonPrefab.transform.parent).GetComponent<CharacterSelectButton>();
            Character chara = ((Character)i);
            Addressables.LoadAssetAsync<Sprite>("icon_"+chara.ToString()).Completed += res => {
                go.Icon.sprite = res.Result;
            };
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
        _titleText.text = $"SELECT {Settings.PLAYER_COUNT} CHARACTERS <size=40>{selectedCharacters.Count}/{Settings.PLAYER_COUNT}</size>";
    }


    public void Select(CharacterSelectButton character)
    {
        var isActive = selectedCharacters.Exists(c => c == character);

        if(isActive)
        {
            selectedCharacters.Remove(character);
        }
        else
        {
            if(selectedCharacters.Count >= Settings.PLAYER_COUNT)
            {
                return;
            }
            selectedCharacters.Add(character);
        }

        isActive = !isActive;
        character.Button.GetComponent<Image>().color = isActive ? new Color(1, 1, 0.3f) : Color.white;
        _startGameButton.gameObject.SetActive(selectedCharacters.Count >= Settings.PLAYER_COUNT);            
    }

    public void StartGame()
    {
        List<Character> clist = new List<Character>();
        selectedCharacters.ForEach(c => clist.Add(c.Character));
        GameManager.Instance.SelectedCharacters(clist);
    }
}