using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager
{

    public delegate void OnUpdateRageMeterEventDelegate(float arg0);
    public delegate void OnHarvetsToolErrorEventDelegate();


    public static OnUpdateRageMeterEventDelegate OnUpdateRageMeterEventHandler = default;

    public static OnHarvetsToolErrorEventDelegate OnHarvetsToolErrorEventHandler = default;


    #region GameLoop Events

    public delegate void GameOverEventDelegate();
    public delegate void GameScoreUpdateDelegate();

    public static GameOverEventDelegate OnGameOverEventHandler = default;
    public static GameScoreUpdateDelegate OnGameScoreUpdatetHandler = default;

    public static void RaiseGameOverEvent()
    {
        if (OnGameOverEventHandler != null)
        {
            OnGameOverEventHandler.Invoke();
        }
    }

    public static void RaiseGameScoreUpdateEvent()
    {
        if (OnGameScoreUpdatetHandler != null)
        {
            OnGameScoreUpdatetHandler.Invoke();
        }
    }
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

    #region Conveyor belt events handlers

    public delegate void OnCBMotionPauseEventDelegate(int cbID);

    public delegate void OnCBMotionResumeEventDelegate(int cbID);

    public delegate void OnCBUpdateSpeedEventDelegate(float speed);


    public static OnCBMotionPauseEventDelegate OnCBMotionPauseEventHandler = default;

    public static OnCBMotionResumeEventDelegate OnCBMotionResumeEventHandler = default;

    public static OnCBUpdateSpeedEventDelegate OnCBUpdateSpeedEventHandler = default;


    #endregion

    public static void RaiseOnConveyorBeltMotionPauseEvent(int cbID)
    {
        if (OnCBMotionPauseEventHandler != null)
        {
            OnCBMotionPauseEventHandler.Invoke(cbID);
        }
    }


    public static void RaiseOnConveyorBeltMotionResumeEvent(int cbID)
    {
        if (OnCBMotionResumeEventHandler != null)
        {
            OnCBMotionResumeEventHandler.Invoke(cbID);
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


    #region InteractionUI Eventsx

    /// Delegate
    public delegate void TriggerEnterInteractableVolumeEventDelegate();

    public delegate void TriggerExitInteractableVolumeEventDelegate();

    // handler
    public static TriggerEnterInteractableVolumeEventDelegate OnTriggerEnterInteractableEventHandler = default;

    public static TriggerExitInteractableVolumeEventDelegate OnTriggerExitInteractableEventHandler = default;

    /// Invoker
    public static void RaiseOnTriggerEnterInteractableVolumeEvent()
    {
        if (OnTriggerEnterInteractableEventHandler != null)
        {
            OnTriggerEnterInteractableEventHandler.Invoke();
        }
    }

    public static void RaiseOnTriggerExitInteractableVolumeEvent()
    {
        if (OnTriggerExitInteractableEventHandler != null)
        {
            OnTriggerExitInteractableEventHandler.Invoke();
        }
    }

    #endregion


    #region Player Tool / Organ events

    public delegate void PlayerPickUpToolEventDelegate(LDEnums.Tools toolType, Sprite sprite, float harvestRate);
    public delegate void PlayerPickUpOrganEventDelegate(LDEnums.OrgansType toolType, Sprite sprite);
    public delegate void PlayerCollectOrganEventDelegate(LDEnums.OrgansType toolType);

    public static PlayerPickUpToolEventDelegate OnPlayerPickUpToolEventHandler = default;
    public static PlayerPickUpOrganEventDelegate OnPlayerPickUpOrganEventHandler = default;
    public static PlayerCollectOrganEventDelegate OnPlayerCollectOrganEventHandler = default;


    public static void RaisePlayerPickUpToolEvent(LDEnums.Tools toolType, Sprite sprite, float harvestRate)
    {
        if (OnPlayerPickUpToolEventHandler != null)
        {
            OnPlayerPickUpToolEventHandler.Invoke(toolType, sprite, harvestRate);
        }
    }

    public static void RaisePlayerPickUpOrganEvent(LDEnums.OrgansType organType, Sprite sprite)
    {
        if (OnPlayerPickUpOrganEventHandler != null)
        {
            OnPlayerPickUpOrganEventHandler.Invoke(organType, sprite);
        }
    }

    public static void RaisePlayerCollectOrganEvent(LDEnums.OrgansType organType)
    {
        if (OnPlayerCollectOrganEventHandler != null)
        {
            OnPlayerCollectOrganEventHandler.Invoke(organType);
        }
    }


    #endregion


}