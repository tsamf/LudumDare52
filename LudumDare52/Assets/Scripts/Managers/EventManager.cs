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
    public delegate void GameScoreUpdateDelegate(float newScore);

    public static GameOverEventDelegate OnGameOverEventHandler = default;
    public static GameScoreUpdateDelegate OnGameScoreUpdatetHandler = default;

    public static void RaiseGameOverEvent()
    {
        if (OnGameOverEventHandler != null)
        {
            OnGameOverEventHandler.Invoke();
        }
    }

    public static void RaiseGameScoreUpdateEvent(float newScore)
    {
        if (OnGameScoreUpdatetHandler != null)
        {
            OnGameScoreUpdatetHandler.Invoke(newScore);
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

    public delegate void ConveyorBeltAvailabilitUpdated(int cbID);


    public static OnCBMotionPauseEventDelegate OnCBMotionPauseEventHandler = default;

    public static OnCBMotionResumeEventDelegate OnCBMotionResumeEventHandler = default;

    public static OnCBUpdateSpeedEventDelegate OnCBUpdateSpeedEventHandler = default;

    public static ConveyorBeltAvailabilitUpdated OnConveyorBeltAvailabilityUpdatedEventHandler = default;


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

    public static void RaiseConveyorBeltAvailabilityUpdatedEvent(int cbID)
    {
        if (OnConveyorBeltAvailabilityUpdatedEventHandler != null)
        {
            OnConveyorBeltAvailabilityUpdatedEventHandler.Invoke(cbID);
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

    public delegate void PlayerPickUpToolEventDelegate(LDEnums.Tools toolType, Sprite sprite, float harvestRate, AudioClip pickupSFX);
    public delegate void PlayerPickUpOrganEventDelegate(LDEnums.OrgansType organType, Sprite sprite, float organScore);
    public delegate void PlayerCollectOrganEventDelegate(LDEnums.OrgansType organType,float score);

    public static PlayerPickUpToolEventDelegate OnPlayerPickUpToolEventHandler = default;
    public static PlayerPickUpOrganEventDelegate OnPlayerPickUpOrganEventHandler = default;
    public static PlayerCollectOrganEventDelegate OnPlayerCollectOrganEventHandler = default;


    public static void RaisePlayerPickUpToolEvent(LDEnums.Tools toolType, Sprite sprite, float harvestRate, AudioClip pickupSFX)
    {
        if (OnPlayerPickUpToolEventHandler != null)
        {
            OnPlayerPickUpToolEventHandler.Invoke(toolType, sprite, harvestRate, pickupSFX);
        }
    }

    public static void RaisePlayerPickUpOrganEvent(LDEnums.OrgansType organType, Sprite sprite, float organScore)
    {
        if (OnPlayerPickUpOrganEventHandler != null)
        {
            OnPlayerPickUpOrganEventHandler.Invoke(organType, sprite, organScore);
        }
    }

    public static void RaisePlayerCollectOrganEvent(LDEnums.OrgansType organType, float score)
    {
        if (OnPlayerCollectOrganEventHandler != null)
        {
            OnPlayerCollectOrganEventHandler.Invoke(organType, score);
        }
    }


    #endregion


    #region Objects Breakdown events

    public delegate void ObjectBreakdownEventDelegate(LDEnums.RepairableObjects repairableObject);
    public delegate void ObjectRepairedEventDelegate(LDEnums.RepairableObjects repairableObject);

    public static ObjectBreakdownEventDelegate OnObjectBreakdownEventHandler;
    public static ObjectBreakdownEventDelegate OnObjectRepairedEventHandler;


    public static void RaiseOnObjectBreakdownEvent(LDEnums.RepairableObjects repairableObject)
    {
        if (OnObjectBreakdownEventHandler != null)
        {
            OnObjectBreakdownEventHandler.Invoke(repairableObject);
        }
    }

    public static void RaiseOnObjectRepairedEvent(LDEnums.RepairableObjects repairableObject)
    {
        if (OnObjectRepairedEventHandler != null)
        {
            OnObjectRepairedEventHandler.Invoke(repairableObject);
        }
    }

    #endregion


    public delegate void TutorialStepCompleted(bool arg0);

    public static TutorialStepCompleted OnTutorialStepCompletedEventHandler = default;

    public static void RaiseOnTutorialStepCompletedEvent(bool value)
    {
        if (OnTutorialStepCompletedEventHandler != null)
        {
            OnTutorialStepCompletedEventHandler.Invoke(value);
        }
    }

}