using System;
using UnityEngine;
using UnityEngine.InputSystem;

using DissassemblyLine.Interfaces;

namespace DissassemblyLine
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class DeadBody : MonoBehaviour, IInteractable, IMoveable, IGrindable
    {
        [Header("Set In Inspector")]

        [SerializeField] internal LDEnums.Interactable interactableType;

        [Header("Set dynamically")]

        [SerializeField] internal LDEnums.BodyType bodyType;
        [SerializeField] internal SpriteRenderer bodySpriteRenderer;
        [SerializeField] internal Organ[] organs;

        [SerializeField] private InteractionButton interactionButton;
        [SerializeField] private Rigidbody2D rigidBody2D;
        [SerializeField] private HarvestBar harvestbar;

        [SerializeField] private InputActionReference interactionInputAction;
        [SerializeField] private AudioClip harvestingSFX;
        [SerializeField] private bool hasPlayed;

        [SerializeField] private int currentConveyorBeltID;

        private bool canInteract = false;
        private float harvestTime = 0f;
        private float timer = 0f;
        private Organ harvestingOrgan;


        #region IInteractable Interface Implementation


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


        #region IMoveable Interface Implementation


        public Action<IMoveable> OnObjectDisabled
        {
            get; set;
        }


        public int ConveyorBeltID
        {
            get => currentConveyorBeltID;
            set => currentConveyorBeltID = value;
        }


        public void MoveOnBelt(float speed)
        {
            if(transform)
                transform.Translate(transform.right * speed * Time.deltaTime);
        }


        #endregion


        #region IGrindable Interface Impelementation


        public void OnGrind()
        {
            if (!organs[0].isHarvested || !organs[1].isHarvested)
            {
                //Debug.LogFormat("Body with organ grinded {0} ", GameManager.instance.maxRageBarValue / GameManager.instance.maxNoOfOrgansGrindingAllowed);
                EventManager.RaiseUpdateRageMeterEvent(GameManager.instance.maxRageBarValue / GameManager.instance.maxNoOfOrgansGrindingAllowed);
            }

            Destroy(gameObject);
        }


        #endregion


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


        private void HandleOrganHarvesting()
        {
            if (canInteract)
            {
                bool keyPressed = interactionInputAction.action.IsPressed();
                if (keyPressed)
                {
                    if (!hasPlayed)
                    {
                        AudioSource.PlayClipAtPoint(harvestingSFX, Camera.main.transform.position);
                        hasPlayed = true;
                    }
                    harvestbar.EnableBar();
                    timer += Time.deltaTime;
                    harvestbar.UpdateBar(timer / harvestTime);

                    if (timer >= harvestTime)
                    {
                        hasPlayed = false;
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
        }


        #region Mono

        private void Awake()
        {
            Debug.Assert(!interactableType.Equals(LDEnums.Interactable.None), name + " interactable type is not assigned in the inspector");

            organs = GetComponentsInChildren<Organ>(true);
            interactionButton = GetComponentInChildren<InteractionButton>();
            harvestbar = GetComponentInChildren<HarvestBar>();
            bodySpriteRenderer = GetComponent<SpriteRenderer>();
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            interactionInputAction = InputManager.instance.interact;
        }

        private void OnDisable()
        {
            interactionInputAction = null;
            //if(cb) cb.RemoveBody(this);
            OnObjectDisabled?.Invoke(this);
            OnObjectDisabled = null;
        }

        private void OnDestroy()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerToolController toolController) && collision.TryGetComponent(out PlayerOrganController organController))
            {
                if (toolController.currentToolInHand.Equals(LDEnums.Tools.None))
                    return;

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

                Debug.LogFormat("Organ hravesting {0}", harvestingOrgan.scriptableObject.name);

                harvestTime = toolController.harvestRate;
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

        void Update()
        {
            HandleOrganHarvesting();
        }
        #endregion
    }
}