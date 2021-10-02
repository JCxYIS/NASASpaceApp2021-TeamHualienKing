using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Facility : MonoBehaviour
{
    // [Header("Bindings")]
    // public 
    SpriteRenderer _spriteRenderer;

    // [Header("Param")]
    Material NormalMaterial;
    Material OutlinedMaterial;

    [Header("Var")]
    [ReadOnly]
    public bool IsTriggerStay;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        NormalMaterial = Resources.Load<Material>("Material/Normal");
        OutlinedMaterial = Resources.Load<Material>("Material/Outline");
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(IsTriggerStay)
            _spriteRenderer.material = OutlinedMaterial;
        else
            _spriteRenderer.material = NormalMaterial;
    }



    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        IsTriggerStay = true;
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        IsTriggerStay = false;
    }
}