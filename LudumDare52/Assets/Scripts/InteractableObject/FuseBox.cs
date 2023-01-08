using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour, IInteractable
{
    [Header("Set In Inspector")]

    [SerializeField] internal LDEnums.Interactable interactableType;

    [Header("Set dynamically")]

    [SerializeField] internal InteractionButton interactionButton;


    #region Interface Implementation

    LDEnums.Interactable IInteractable.interactableType
    {
        get => interactableType;
    }

    public void OnEnterInteractableTriggerVolume()
    {
        if (interactionButton)
        {
            interactionButton.OnEnableCanvasComponent();
        }
    }

    public void OnExitInteractableTriggerVolume()
    {
        if (interactionButton)
        {
            interactionButton.OnDisableCanvasComponent();
        }
    }

    #endregion

    void Awake()
    {
        Debug.Assert(!interactableType.Equals(LDEnums.Interactable.None), name + " interactable type is not assigned in the inspector");
        interactionButton = GetComponentInChildren<InteractionButton>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MovementController movementController))
        {
            OnEnterInteractableTriggerVolume();
            /// Show Interaction button on UI
            /// Send the information of organ if the player can harvest if he can, based on the tool he is holding
            /// timer for harvesting
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MovementController movementController))
        {
            OnExitInteractableTriggerVolume();
        }
    }
}
