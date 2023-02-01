using System.Collections.Generic;
using UnityEngine;
using DissassemblyLine.Interfaces;

namespace DissassemblyLine
{
    public class ConveyorBelt : MonoBehaviour
    {
        #region Properties

        public Transform BodySpawnLocation { get => bodySpawnLocation; }
        public int ConveyorBeltID { get => conveyorBeltID; set => conveyorBeltID = value; }

        #endregion

        #region Attributes

        [Header("Set in Inspector")]

       
        [SerializeField] private Transform bodySpawnLocation;


        [Header("Set Dynamically")]

        [Range(0, 2)]
        [SerializeField] private int conveyorBeltID = 0;

        /// <summary>
        /// Status updates dynamically during runtime e.g when player interacting with the body to harvest organ this will be set to paused
        /// </summary>
        [SerializeField] private LDEnums.ConveyorBeltMotionStatus motionStatus = LDEnums.ConveyorBeltMotionStatus.Active;

        private List<IMoveable> moveableOnBelt = null;
        private const float pauseSpeed = 0;
        private float currentSpeed = 0;

        #endregion

        private void EnableMotion(int cbID)
        {
            if (!ConveyorBeltID.Equals(cbID))
                return;

            //Debug.LogFormat("EnableConveyor motion {0}", cbID);
            OnUpdateMotionStatus(LDEnums.ConveyorBeltMotionStatus.Active);
            OnUpdateSpeed(GameManager.CurrentConveyorBeltSpeed);
        }


        private void PauseMotion(int cbID)
        {
            if (!ConveyorBeltID.Equals(cbID))
                return;

            OnUpdateMotionStatus(LDEnums.ConveyorBeltMotionStatus.Paused);
            OnUpdateSpeed(pauseSpeed);
        }


        private void OnUpdateSpeed(float newSpeed)
        {
            currentSpeed = newSpeed;
        }


        private void OnUpdateMotionStatus(LDEnums.ConveyorBeltMotionStatus newStatus)
        {
            motionStatus = newStatus;
        }


        private void Awake()
        {
            Debug.Assert(BodySpawnLocation != null, name + " conveyor belt is missing body spawn location in the inspector");
            moveableOnBelt = new List<IMoveable>();
        }


        private void OnEnable()
        {
            /// Register events
            EventManager.OnCBMotionResumeEventHandler += EnableMotion;
            EventManager.OnCBMotionPauseEventHandler += PauseMotion;
            EventManager.OnCBUpdateSpeedEventHandler += OnUpdateSpeed;
        }


        private void OnDisable()
        {
            EventManager.OnCBMotionResumeEventHandler -= EnableMotion;
            EventManager.OnCBMotionPauseEventHandler -= PauseMotion;
            EventManager.OnCBUpdateSpeedEventHandler -= OnUpdateSpeed;
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IMoveable moveable))
            {
                //moveable.SetCB(this);
                moveable.OnObjectDisabled += RemoveBody;
                AddBody(moveable);
            }
        }


        private void AddBody(IMoveable moveable)
        {
            if (!moveableOnBelt.Contains(moveable))
            {
                moveableOnBelt.Add(moveable);
            }
        }


        private void RemoveBody(IMoveable moveable)
        {
            if (moveableOnBelt.Contains(moveable))
            {
                moveableOnBelt.Remove(moveable);
            }
        }


        private void Update()
        {
            if (moveableOnBelt != null && GameManager.instance.gameState.Equals(LDEnums.GameState.Running))
            {
                for (int indx = 0; indx < moveableOnBelt.Count; indx++)
                {
                    moveableOnBelt[indx].MoveOnBelt(currentSpeed);
                }
            }
        }
    }
}
