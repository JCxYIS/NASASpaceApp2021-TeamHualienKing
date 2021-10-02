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

    [HideInInspector] 
    public PlayerController PlayerController;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
    }

    public void Init(Character character)
    {
        Character = character;

        Addressables.LoadAssetAsync<Sprite>("sprite_"+character.ToString()).Completed += res => {
            GetComponent<SpriteRenderer>().sprite = res.Result;
        };
        
        HpMax = 100;
        Hp = HpMax;
    }
}