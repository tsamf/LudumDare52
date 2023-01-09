using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolController : MonoBehaviour
{
    [Header("Set Dynamically")]

    [SerializeField] internal LDEnums.Tools currentToolInHand;
    [SerializeField] internal float harvestRate;
    [SerializeField] internal PlayerTool playerToolComponent;


    internal void OnPickUpTool(LDEnums.Tools newTool, Sprite toolSprite, float harvestRate, AudioClip pickupSFX)
    {
        currentToolInHand = newTool;
        this.harvestRate = harvestRate;
        playerToolComponent.ChangeToolImage(toolSprite);
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
    }

    private void Awake()
    {
        playerToolComponent = GetComponentInChildren<PlayerTool>();
        Debug.Assert(playerToolComponent != null, name + " is unable to get player tool under child component");
    }

    private void OnEnable()
    {
        EventManager.OnPlayerPickUpToolEventHandler += OnPickUpTool;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerPickUpToolEventHandler -= OnPickUpTool;
    }

    private void Start()
    {
        OnPickUpTool(LDEnums.Tools.None, null, 0, null); // initalize
    }
}
