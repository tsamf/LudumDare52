using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public LDEnums.Interactable interactableType
    {
        get;
    }

    //public void SetInteractableType();

    public void OnEnterInteractableTriggerVolume();

    public void OnExitInteractableTriggerVolume();

}
