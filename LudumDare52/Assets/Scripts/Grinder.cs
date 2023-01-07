using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    [Range(-1,2)]
    [SerializeField] private int grinderID = -1;

    private void Awake()
    {
        Debug.Assert(grinderID != -1, name + " grinder Id is not set in the inspector");
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DeadBody deadBody))
        {
            EventManager.RaiseBodyGrindedEvent(deadBody.currentConveyorBeltID);
            Destroy(deadBody.gameObject);
        }
    }

}
