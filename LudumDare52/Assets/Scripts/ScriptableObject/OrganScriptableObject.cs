using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "OrganSO", menuName = "OrganScriptableObject")]
public class OrganScriptableObject : ScriptableObject
{
    public LDEnums.OrgansType organType;
    public LDEnums.Tools toolToUse;
    public Sprite organSprite;
    public float decayRate;
    public int score;
}
