using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class DeadBody : MonoBehaviour, IInteractable
{
    [Header("Set In Inspector")]

    [SerializeField] internal LDEnums.Interactable interactableType;

    //[SerializeField] internal SpriteRenderer organOneSpriteRenderer;
    //[SerializeField] internal SpriteRenderer organTwoSpriteRenderer;

    [Header("Set dynamically")]

    [SerializeField] internal SpriteRenderer bodySpriteRenderer;
    [SerializeField] internal InteractionButton interactionButton;
    [SerializeField] internal Rigidbody2D rigidBody2D;
    [SerializeField] internal Organ[] organs;
    [SerializeField] internal LDEnums.BodyType bodyType;
    /// Which conveyor belt this body is on
    [SerializeField] internal int currentConveyorBeltID;


    #region Interface Implementation

    /// Interaface IInteractable Implementation
    LDEnums.Interactable IInteractable.interactableType 
    {
        get => interactableType;
    }

    public void OnEnterInteractableTriggerVolume()
    {
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.bodyType = RigidbodyType2D.Kinematic;

        EventManager.RaiseOnConveyorBeltMotionPauseEvent(currentConveyorBeltID);

        if (interactionButton)
        {
            interactionButton.OnEnableCanvasComponent();
        }
    }

    public void OnExitInteractableTriggerVolume()
    {
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        EventManager.RaiseOnConveyorBeltMotionResumeEvent(currentConveyorBeltID);

        if (interactionButton)
        {
            interactionButton.OnDisableCanvasComponent();
        }
    }

    #endregion


    #region Mono

    private void Awake()
    {
        Debug.Assert(!interactableType.Equals(LDEnums.Interactable.None), name + " interactable type is not assigned in the inspector");

        organs              = GetComponentsInChildren<Organ>();
        interactionButton   = GetComponentInChildren<InteractionButton>();
        bodySpriteRenderer  = GetComponent<SpriteRenderer>();
        rigidBody2D         = GetComponent<Rigidbody2D>();
    

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

    #endregion
}
