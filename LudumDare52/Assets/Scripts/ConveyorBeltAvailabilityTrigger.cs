using DissassemblyLine.Interfaces;
using UnityEngine;

namespace DissassemblyLine
{

    public class ConveyorBeltAvailabilityTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IMoveable moveable))
            {
                EventManager.RaiseConveyorBeltAvailabilityUpdatedEvent(moveable.ConveyorBeltID);
            }
        }
    }
}
