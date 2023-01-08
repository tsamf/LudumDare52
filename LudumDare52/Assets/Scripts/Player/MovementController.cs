using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class MovementController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    private float currentSpeed;

    [Header("Dash")]
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashCoolDown = 1f;
    [SerializeField] float dashSpeed = 10f;
    private float TimeSinceLastDash = 0;
    private bool dashed = false;


    [Header("Sprite padding")]
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Vector2 minBounds;
    Vector2 maxBounds;

    private InputActionReference movement;
    private InputActionReference dash;
    private InputActionReference interact;


    private void OnEnable()
    {
        movement = InputManager.instance.movement;
        dash        = InputManager.instance.dash;
        interact    = InputManager.instance.interact;
    }


    private void OnDisable()
    {
        movement = null;
        dash = null;
        interact = null;
    }

    void Start()
    {
        currentSpeed = moveSpeed;
        InitBounds();
    }

 
    void Update()
    {
        Move();
        Dash();
    }

    private void Dash()
    {
        if(!dashed)
        {
            if(dash.action.IsPressed())
            {
                StartCoroutine(Dashing());
            }
        }
        else
        {
            TimeSinceLastDash += Time.deltaTime;
            if(TimeSinceLastDash > dashCoolDown)
            {
                dashed = false;
                TimeSinceLastDash = 0f;
            }
        }
    }

    IEnumerator Dashing()
    {
        currentSpeed = dashSpeed;
        dashed = true;
        yield return new  WaitForSeconds(dashTime);
        currentSpeed = moveSpeed;
    }

    private void Move()
    {
        Vector3 rawInput = movement.action.ReadValue<Vector2>();
        Vector2 delta = rawInput * currentSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

     void InitBounds()
    {
       minBounds = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
       maxBounds = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
    }
}
