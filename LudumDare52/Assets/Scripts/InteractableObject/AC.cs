using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AC : MonoBehaviour, IInteractable, IRepairable
{
    [Header("Set In Inspector")]

    [SerializeField] internal LDEnums.Interactable interactableType;

    [SerializeField] private RepairableScriptableObject acScriptableObject;

    [Header("Set dynamically")]

    [SerializeField] internal InteractionButton interactionButton;

    [SerializeField] internal InputActionReference interactionInputAction;

    [SerializeField] internal HarvestBar harvestbar;

    private Animator animator;
    private float timer = 0f;
    private bool canInteract = false;
    private int isBrokenHash = -1;

    private const string ISBROKEN = "IsBroken";


    #region Interface Implementation

    LDEnums.Interactable IInteractable.interactableType
    {
        get => interactableType;
    }

    public void OnEnterInteractableTriggerVolume()
    {
        if (interactionButton)
        {
            canInteract = true;
            interactionButton.OnEnableCanvasComponent();         
        }
    }

    public void OnExitInteractableTriggerVolume()
    {
        if (interactionButton)
        {
            canInteract = false;
            interactionButton.OnDisableCanvasComponent();
        }
    }


    #endregion


    private void OnObjectBreakdown(LDEnums.RepairableObjects breakdownObject)
    {
        if (breakdownObject.Equals(acScriptableObject.repairableType))
        {
            OnAcBreakdown();
        }
    }


    private void OnAcBreakdown()
    {
        Debug.LogFormat(" AC broke down ");
        animator.SetBool(isBrokenHash, true);
    }


    private void OnAcFixed()
    {
        Debug.LogFormat(" AC fixed ");
        EventManager.RaiseOnObjectRepairedEvent(acScriptableObject.repairableType);
        animator.SetBool(isBrokenHash, false);
    }


    private void OnEPressed()
    {
        if (!canInteract)
            return;

        bool keyPressed = interactionInputAction.action.IsPressed();
        if (keyPressed)
        {
            harvestbar.EnableBar();
            timer += Time.deltaTime;
            harvestbar.UpdateBar(timer/acScriptableObject.repairDuration);

            if (timer >= acScriptableObject.repairDuration)
            {
                canInteract = false;
                harvestbar.DisableBar();
                OnAcFixed();
            }
        }
        else
        {
            timer = 0;
            harvestbar.DisableBar();
        }
    }


    void Awake()
    {
        Debug.Assert(!interactableType.Equals(LDEnums.Interactable.None), name + " interactable type is not assigned in the inspector");
        Debug.Assert(acScriptableObject!= null, name + " is missing ac scriptable object reference in the inspector");
        interactionButton   = GetComponentInChildren<InteractionButton>(); 
        harvestbar          = GetComponentInChildren<HarvestBar>();
        animator            = GetComponentInChildren<Animator>();
        isBrokenHash        = Animator.StringToHash(ISBROKEN);
    }


    void OnEnable()
    {
        interactionInputAction = InputManager.instance.interact;
        EventManager.OnObjectBreakdownEventHandler += OnObjectBreakdown;
    }


    void OnDisable()
    {
        EventManager.OnObjectBreakdownEventHandler -= OnObjectBreakdown;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MovementController movementController))
        {
            OnEnterInteractableTriggerVolume();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MovementController movementController))
        {
            OnExitInteractableTriggerVolume();
        }
    }


    private void Update()
    {
         OnEPressed();
    }
}
