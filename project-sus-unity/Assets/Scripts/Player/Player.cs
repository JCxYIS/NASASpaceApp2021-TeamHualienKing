using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// A single unit that player control
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [Header("Variables")]
    public Character character;

    [HideInInspector] 
    public PlayerController PlayerController;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
    }
}