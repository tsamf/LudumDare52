using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "BodySO",menuName = "BodyScriptableObject")]
public class BodiesScriptableObject : ScriptableObject
{
    public LDEnums.BodyType bodyType;
    public Sprite bodySprite;
    public List<OrganScriptableObject> listOfOrgans;
    public Color color;
}
