using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrganController : MonoBehaviour
{
    [Header("Set Dynamically")]

    [SerializeField] internal LDEnums.OrgansType currentOrganInHand;
    [SerializeField] internal float currentOrganScore;
    [SerializeField] internal PlayerOrgan playerOrganComponent;


    internal void OnPickUpOrgan(LDEnums.OrgansType newOrgan, Sprite organSprite, float organScore)
    {
        currentOrganInHand = newOrgan;
        currentOrganScore = organScore;
        playerOrganComponent.ChangeOrganImage(organSprite);
    }

    internal void OnOrganCollected(LDEnums.OrgansType _organ, float _score)
    {
        currentOrganInHand = LDEnums.OrgansType.None;
        currentOrganScore = 0f;
        playerOrganComponent.ChangeOrganImage(null);
    }


    private void Awake()
    {
        playerOrganComponent = GetComponentInChildren<PlayerOrgan>();
        Debug.Assert(playerOrganComponent != null, name + " is unable to get player organ under child component");
    }

    private void OnEnable()
    {
        EventManager.OnPlayerPickUpOrganEventHandler += OnPickUpOrgan;
        EventManager.OnPlayerCollectOrganEventHandler += OnOrganCollected;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerPickUpOrganEventHandler -= OnPickUpOrgan;
        EventManager.OnPlayerCollectOrganEventHandler -= OnOrganCollected;
    }


    private void Start()
    {
        OnPickUpOrgan(LDEnums.OrgansType.None, null, 0); // initalize
    }

}
