using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RepairableScriptableObjectSO", menuName = "RepairableScriptableObject")]
public class RepairableScriptableObject : ScriptableObject
{
    public LDEnums.RepairableObjects repairableType;
    public float repairDuration;
}
