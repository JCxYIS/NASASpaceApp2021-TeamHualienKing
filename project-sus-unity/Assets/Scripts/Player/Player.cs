using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

/// <summary>
/// A single unit that player control
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [Header("Variables")]
    [ReadOnly]
    public Character Character;

    [ReadOnly]
    public int Hp;

    [ReadOnly]
    public int HpMax;

    [ReadOnly]
    public bool IsControlling;

    [HideInInspector] 
    public PlayerController PlayerController;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        DontDestroyOnLoad(gameObject);
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
    }


    public void AddHealth(int add)
    {
        Hp += add;
        if(Hp > HpMax)
            Hp = HpMax;
        if(Hp <= 0)
            GameController.Instance.GameOver();
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
            facility.Trigger();
        }
    }
}