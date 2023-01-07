using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    internal static InputManager instance;

    public InputActionAsset inputActionAsset;

    [Header("Input Action Reference")]

    [SerializeField] internal InputActionReference movement;
    [SerializeField] internal InputActionReference dash;
    [SerializeField] internal InputActionReference interact;


    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if(inputActionAsset != null) inputActionAsset.Enable();
    }

    private void OnDisable()
    {
        if(inputActionAsset != null) inputActionAsset.Disable();
    }

}
