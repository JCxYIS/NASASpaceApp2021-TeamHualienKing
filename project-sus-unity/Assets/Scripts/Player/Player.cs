using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System;

/// <summary>
/// A single unit that player control
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] GameObject _uiObjectPrefab;

    [Header("Variables")]
    [ReadOnly]
    public Character Character;

    [ReadOnly]
    public int Hp;

    [ReadOnly]
    public int HpMax;

    [ReadOnly]
    public bool IsControlling;

    [ReadOnly]
    public PlayerUiObject UiObject;

    [ReadOnly] 
    public PlayerController PlayerController;

    [ReadOnly]
    public float InteractTime;

    [ReadOnly]
    public float InteractTimeMax;

    [ReadOnly]
    public Facility TriggeringFacility;

    /// <summary>
    /// If this player is controlled, and interact with some facility.
    /// Has synced in update time
    /// </summary>
    public Action<Facility> OnTriggerFacilityStay;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(TriggeringFacility)
        {
            TriggeringFacility.Trigger();
            OnTriggerFacilityStay?.Invoke(TriggeringFacility);
        }
    }

    public void Init(Character character)
    {
        Character = character;
        name = Character.ToString();

        Addressables.LoadAssetAsync<Sprite>("sprite_"+character.ToString()).Completed += res => {
            GetComponent<SpriteRenderer>().sprite = res.Result;
        };
        
        HpMax = 100;
        Hp = HpMax;

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        UiObject = Instantiate(_uiObjectPrefab, canvas.transform).GetComponent<PlayerUiObject>();
        UiObject.Player = this;
        UiObject.CanvasRect = canvas.GetComponent<RectTransform>();
    }




    public void AddHealth(int add)
    {
        Hp += add;
        if(Hp > HpMax)
            Hp = HpMax;
        if(Hp <= 0)
            GameController.Instance.GameOver();
    }

    public void UpdateInteractTimeMax(float tmax)
    {
        InteractTimeMax = tmax;
    }

    public bool TickInteractTime()
    {
        InteractTime += Time.deltaTime;
        if(InteractTime >= InteractTimeMax)
        {
            ResetInteractTime();
            return true;
        }
        return false;
    }

    public void ResetInteractTime()
    {
        InteractTime = 0;
        InteractTimeMax = 0;
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if(!IsControlling)
            return;

        Facility facility = other.GetComponent<Facility>();
        if(facility)
        {
            TriggeringFacility = facility;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if(!IsControlling)
            return;

        Facility facility = other.GetComponent<Facility>();
        if(facility)
        {
            TriggeringFacility = null;
        }
    }
}