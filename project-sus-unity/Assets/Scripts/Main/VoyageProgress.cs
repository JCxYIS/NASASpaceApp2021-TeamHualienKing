using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VoyageProgress : MonoBehaviour
{
    [SerializeField] Slider slider;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        slider.value = GameController.Instance.Progress;
    }
}