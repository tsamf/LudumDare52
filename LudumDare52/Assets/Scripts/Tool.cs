using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tool : MonoBehaviour, IInteractable
{
    [Header("Set In Inspector")]
    [SerializeField] private LDEnums.Tools toolType;
    [SerializeField] private LDEnums.Interactable interactableType;
    [SerializeField] private ToolScriptableObject toolScriptableObject;


    [Header("Set dynamically")]
    [SerializeField] internal InteractionButton interactionButton;
    [SerializeField] internal InputActionReference interactionInputAction;

    public bool isInteracting = false;

    /// Interaface IInteractable Implementation
    LDEnums.Interactable IInteractable.interactableType
    {
        get => interactableType;
    }

    public void OnEnterInteractableTriggerVolume()
    {
        if (interactionButton)
        {
            isInteracting = true;
            interactionButton.OnEnableCanvasComponent();
        }
    }

    public void OnExitInteractableTriggerVolume()
    {
        if (interactionButton)
        {
            isInteracting = false;
            interactionButton.OnDisableCanvasComponent();
        }
    }

    #region Mono

    private void Awake()
    {
        interactionButton = GetComponentInChildren<InteractionButton>();

        Debug.Assert(!interactableType.Equals(LDEnums.Interactable.None), name + " interactable type is not assigned in the inspector");
        Debug.Assert(!toolType.Equals(LDEnums.Tools.None), name + "tool type is not assigned in the inspector");
        Debug.Assert(toolScriptableObject != null, name + " is missing scriptable obejct reference in the inspector");
    }


    void OnEnable()
    {
        interactionInputAction = InputManager.instance.interact;
    }

    void OnDisable()
    {
        interactionInputAction = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteracting)
        {
            bool keyPressed = interactionInputAction.action.WasPressedThisFrame();
            if (keyPressed)
            {
                Debug.LogFormat("E pressed {0}", toolType.ToString());
                /// swap tool in the player tool
                EventManager.RaisePlayerPickUpToolEvent(toolScriptableObject.toolType, toolScriptableObject.toolSprite, toolScriptableObject.harvestTime, toolScriptableObject.pickupSFX);
            }
        }
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

    #endregion
}
