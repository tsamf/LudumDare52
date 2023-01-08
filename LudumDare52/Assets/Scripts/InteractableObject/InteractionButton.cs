using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class InteractionButton : MonoBehaviour
{

    [Header("Set Dynamically")]

    [SerializeField] internal TextMeshProUGUI textMeshProUGUI;

    [SerializeField] internal Canvas canvas;


    internal void OnEnableCanvasComponent()
    {
        canvas.enabled = true;
    }

    internal void OnDisableCanvasComponent()
    {
        canvas.enabled = false;
    }

    internal void OnChangeText(string value)
    {
        textMeshProUGUI.text = value;
    }


    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
