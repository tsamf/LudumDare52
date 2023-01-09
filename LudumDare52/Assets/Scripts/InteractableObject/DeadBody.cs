using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] internal HarvestBar harvestbar;
    [SerializeField] internal LDEnums.BodyType bodyType;
    /// Which conveyor belt this body is on
    [SerializeField] internal int currentConveyorBeltID;
    [SerializeField] internal InputActionReference interactionInputAction;

    public bool canInteract = false;
    public bool isHarvesting = false;
    public float harvestTime = 0f;
    public float timer = 0f;
    public Organ harvestingOrgan;
    private ConveyorBelt conveyorbelt = null;

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
            canInteract = true;
            interactionButton.OnEnableCanvasComponent();
        }
    }

    public void OnExitInteractableTriggerVolume()
    {
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        EventManager.RaiseOnConveyorBeltMotionResumeEvent(currentConveyorBeltID);

        if (interactionButton)
        {
            canInteract = false;
            interactionButton.OnDisableCanvasComponent();
        }
    }

    #endregion


    #region Mono

    private void Awake()
    {
        Debug.Assert(!interactableType.Equals(LDEnums.Interactable.None), name + " interactable type is not assigned in the inspector");

        organs              = GetComponentsInChildren<Organ>(true);
        interactionButton   = GetComponentInChildren<InteractionButton>();
        harvestbar          = GetComponentInChildren<HarvestBar>();
        bodySpriteRenderer  = GetComponent<SpriteRenderer>();
        rigidBody2D         = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        interactionInputAction = InputManager.instance.interact;
    }

    private void OnDisable()
    {
        interactionInputAction = null;
    }

    /// <summary>
    /// Returns organ matching the tool which is not harvested 
    /// </summary>
    /// <param name="toolInHand"></param>
    /// <param name="organs"></param>
    /// <returns></returns>
    private Organ GetOrganMatchingTool(LDEnums.Tools toolInHand, Organ[] organs)
    {
        for (int index = 0; index < organs.Length; index++)
        {
            if (organs[index].toolToUse.Equals(toolInHand) && !organs[index].isHarvested)
            {
                return organs[index];
            }
        }

        return null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ConveyorBelt cb))
        {
            conveyorbelt = cb;
        }

        if (collision.TryGetComponent(out PlayerToolController toolController) && collision.TryGetComponent(out PlayerOrganController organController))
        {

            if (!organController.currentOrganInHand.Equals(LDEnums.OrgansType.None))
                return;

            /// check current tool player has
            /// then pick the organ matching the tool
            harvestingOrgan = GetOrganMatchingTool(toolController.currentToolInHand, organs);
            if (harvestingOrgan == null)
            {
                harvestTime = 0;
                return;
            }

            Debug.LogFormat("Organ hravesting {0}", harvestingOrgan.name);

            harvestTime = toolController.harvestRate;
            OnEnterInteractableTriggerVolume();

            /// Show Interaction button on UI
            /// Send the information of organ if the player can harvest if he can, based on the tool he is holding
            /// timer for harvesting
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ConveyorBelt cb))
        {
            conveyorbelt = null;
        }

        if (collision.TryGetComponent(out MovementController movementController))
        {
            OnExitInteractableTriggerVolume();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent( out ConveyorBelt cb))
        {
        }
    }

    public float cbMoveSpeed = 0;

    void Update()
    {
        if (canInteract)
        {
            bool keyPressed = interactionInputAction.action.IsPressed();
            if (keyPressed)
            {
                harvestbar.EnableBar();
                timer += Time.deltaTime;
                harvestbar.UpdateBar(timer/harvestTime);

                if (timer >= harvestTime)
                {
                    canInteract = false;
                    harvestbar.DisableBar();
                    harvestingOrgan.OnOrganPickedUP();
                    if (harvestingOrgan)
                    {
                        Debug.LogFormat("organ {0}, Score... {1}", harvestingOrgan.scriptableObject.organType, harvestingOrgan.currentScore);

                        EventManager.RaisePlayerPickUpOrganEvent(harvestingOrgan.scriptableObject.organType, harvestingOrgan.scriptableObject.organSprite, harvestingOrgan.currentScore);
                    }
                }
            }
            else
            {
                timer = 0;
                harvestbar.DisableBar();
            }
        }

        if (conveyorbelt != null)
        {
            transform.Translate(transform.right * conveyorbelt.currentSpeed * Time.deltaTime);
        }

    }

    #endregion
}
