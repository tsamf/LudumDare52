using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    [Range(-1,2)]
    [SerializeField] private int grinderID = -1;

    [SerializeField] private AudioClip grindingSFX;

    private void Awake()
    {
        Debug.Assert(grinderID != -1, name + " grinder Id is not set in the inspector");
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DeadBody deadBody))
        {
            EventManager.RaiseBodyGrindedEvent(deadBody.currentConveyorBeltID);

            if (!deadBody.organs[0].isHarvested || !deadBody.organs[1].isHarvested)
            {
                Debug.LogFormat("Body with organ grinded {0} ", GameManager.instance.maxRageBarValue / GameManager.instance.maxNoOfOrgansGrindingAllowed);
                EventManager.RaiseUpdateRageMeterEvent(GameManager.instance.maxRageBarValue/ GameManager.instance.maxNoOfOrgansGrindingAllowed);
            }

            AudioSource.PlayClipAtPoint(grindingSFX, Camera.main.transform.position);
            Destroy(deadBody.gameObject);
        }
    }

}
