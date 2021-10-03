using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Facility : MonoBehaviour
{
    // [Header("Bindings")]
    // public 
    SpriteRenderer _spriteRenderer;

    [Header("Param")]
    public string Name = "";
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
        name = $"FACILITY - {Name}";
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(IsTriggerStay)
            _spriteRenderer.material = OutlinedMaterial;
        else
            _spriteRenderer.material = NormalMaterial;
        
        IsTriggerStay = false;
    }

    public void Trigger()
    {
        IsTriggerStay = true;
    }
}