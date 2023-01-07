using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager
{

    public delegate void OnUpdateRageMeterEventDelegate(float arg0);
    public delegate void OnHarvetsToolErrorEventDelegate();


    public static OnUpdateRageMeterEventDelegate OnUpdateRageMeterEventHandler = default;

    public static OnHarvetsToolErrorEventDelegate OnHarvetsToolErrorEventHandler = default;


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

}