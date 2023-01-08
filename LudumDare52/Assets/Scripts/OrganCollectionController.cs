using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganCollectionController : MonoBehaviour
{
    [Header("Set Dynamically")]
    [SerializeField] internal List<LDEnums.OrgansType> organsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerOrganController playerOrganController) && !playerOrganController.currentOrganInHand.Equals(LDEnums.OrgansType.None))
        {
            ///
            organsCollected.Add(playerOrganController.currentOrganInHand);
            EventManager.RaisePlayerCollectOrganEvent(playerOrganController.currentOrganInHand);
        }
    }
}
