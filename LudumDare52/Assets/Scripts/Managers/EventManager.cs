using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager
{

    public delegate void OnUpdateRageMeterEventDelegate(float arg0);
    public delegate void OnHarvetsToolErrorEventDelegate();


    public static OnUpdateRageMeterEventDelegate OnUpdateRageMeterEventHandler = default;

    public static OnHarvetsToolErrorEventDelegate OnHarvetsToolErrorEventHandler = default;


    #region Conveyor belt events handlers

    public delegate void OnCBMotionPauseEventDelegate();

    public delegate void OnCBMotionResumeEventDelegate();

    public delegate void OnCBUpdateSpeedEventDelegate(float speed);


    public static OnCBMotionPauseEventDelegate OnCBMotionPauseEventHandler = default;

    public static OnCBMotionResumeEventDelegate OnCBMotionResumeEventHandler = default;

    public static OnCBUpdateSpeedEventDelegate OnCBUpdateSpeedEventHandler = default;


    #endregion


    #region Grinder events handlers

    public delegate void OnBodyGrindedEventDelegate(int bodyID);

    public static OnBodyGrindedEventDelegate OnBodyGrindedEventHandler = default;

    #endregion

    /// add events


    public static void RaiseUpdateRageMeterEvent(float arg0)
    {
        if (OnUpdateRageMeterEventHandler != null)
        {
            OnUpdateRageMeterEventHandler.Invoke(arg0);
        }
    }


    public static void RaiseHarvestToolErrorEvent()
    {
        if (OnHarvetsToolErrorEventHandler != null)
        {
            OnHarvetsToolErrorEventHandler.Invoke();
        }
    }

    #region Conveyor belt events region
    /// Conveyor Belt Events
    /// 

    public static void RaiseOnConveyorBeltMotionPauseEvent()
    {
        if (OnCBMotionPauseEventHandler != null)
        {
            OnCBMotionPauseEventHandler.Invoke();
        }
    }


    public static void RaiseOnConveyorBeltMotionResumeEvent()
    {
        if (OnCBMotionResumeEventHandler != null)
        {
            OnCBMotionResumeEventHandler.Invoke();
        }
    }


    public static void RaiseOnCBUpdateSpeedEvent(float speed)
    {
        if (OnCBUpdateSpeedEventHandler != null)
        {
            OnCBUpdateSpeedEventHandler.Invoke(speed);
        }
    }

    #endregion


    #region Grinder events region

    public static void RaiseBodyGrindedEvent(int arg0)
    {
        if (OnBodyGrindedEventHandler != null)
        {
            OnBodyGrindedEventHandler.Invoke(arg0);
        }
    }

    #endregion

}