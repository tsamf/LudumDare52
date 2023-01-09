using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolSO", menuName = "ToolScriptableObject")]
public class ToolScriptableObject : ScriptableObject
{
    public LDEnums.Tools toolType;
    public LDEnums.OrgansType organType;
    public Sprite toolSprite;
    public int harvestTime;
    public AudioClip pickupSFX;
}
