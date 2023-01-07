using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class MovementController : MonoBehaviour
{
    private InputActionReference movement;
    private InputActionReference dash;
    private InputActionReference interact;


    private void Movement(CallbackContext context)
    {


    }


    private void OnEnable()
    {
        movement    = InputManager.instance.movement;
        dash        = InputManager.instance.dash;
        interact    = InputManager.instance.interact;

        movement.action.performed += Movement;
    }


    private void OnDisable()
    {
        movement    = null;
        dash        = null;
        interact    = null;

        movement.action.performed -= Movement;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 value = movement.action.ReadValue<Vector2>();

    }
}
