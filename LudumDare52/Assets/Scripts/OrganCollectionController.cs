using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganCollectionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerOrganController playerOrganController) && !playerOrganController.currentOrganInHand.Equals(LDEnums.OrgansType.None))
        {
            EventManager.RaisePlayerCollectOrganEvent(playerOrganController.currentOrganInHand, playerOrganController.currentOrganScore);
        }
    }
}
