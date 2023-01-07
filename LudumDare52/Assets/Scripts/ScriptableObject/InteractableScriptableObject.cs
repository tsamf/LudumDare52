using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InteractableObjectSO", menuName = "InteractableScriptableObject")]
public class InteractableScriptableObject : ScriptableObject
{
    public LDEnums.Interactable interactableType;
    public float fixRate;
}
