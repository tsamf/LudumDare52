using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SurfaceEffector2D))]
public class ConveyorBelt : MonoBehaviour
{
    [Header("Set in Inspector")]

    [Range(0,2)]
    [SerializeField] internal int conveyorBeltID = 0;

    [SerializeField] internal Transform bodySpawnLocation;

    [SerializeField] private float speed;

    /// <summary>
    /// Status updates dynamically during runtime e.g when player interacting with the body to harvest organ this will be set to paused
    /// </summary>
    [SerializeField] private LDEnums.ConveyorBeltMotionStatus motionStatus = LDEnums.ConveyorBeltMotionStatus.Active;


    [Header("Set Dynamically")]

    [SerializeField] private SurfaceEffector2D surfaceEffector2D = default;

    private const float pauseSpeed = 0;
    private float currentSpeed = 0;


    public void EnableMotion(int cbID)
    {
        if (!conveyorBeltID.Equals(cbID))
            return;

        OnUpdateMotionStatus(LDEnums.ConveyorBeltMotionStatus.Active);
        OnUpdateSpeed(speed);
    }


    public void PauseMotion(int cbID)
    {
        if (!conveyorBeltID.Equals(cbID))
            return;

        OnUpdateMotionStatus(LDEnums.ConveyorBeltMotionStatus.Paused);
        OnUpdateSpeed(pauseSpeed);
    }


    public void OnUpdateSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
        if(surfaceEffector2D) surfaceEffector2D.speed = currentSpeed;
    }


    private void OnUpdateMotionStatus(LDEnums.ConveyorBeltMotionStatus newStatus)
    {
        motionStatus = newStatus;
    }


    private void Awake()
    {
        Debug.Assert(bodySpawnLocation != null, name + " conveyor belt is missing body spawn location in the inspector");

        surfaceEffector2D = GetComponent<SurfaceEffector2D>();
    }

    private void Start()
    {
        speed = GameManager.instance.conveyorBeltSpeed;
        OnUpdateSpeed(speed);
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
}
