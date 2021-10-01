using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CharacterSelectButton : MonoBehaviour
{
    public Image Icon;
    public TMP_Text Name;
    public TMP_Text Desc;


    [HideInInspector] public Character Character;
    private Button _button;
    public Button Button 
    {
        get
        {
            if(_button == null) 
                _button = GetComponent<Button>();
            return _button;
        }
    }
}