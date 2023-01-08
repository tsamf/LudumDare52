using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    [SerializeField] internal LDEnums.BodyType bodyType;
    [SerializeField] internal SpriteRenderer bodySpriteRenderer;

    [SerializeField] internal Organ[] organs;

    //[SerializeField] internal SpriteRenderer organOneSpriteRenderer;
    //[SerializeField] internal SpriteRenderer organTwoSpriteRenderer;

    [Header("Set dynamically")]
    [SerializeField] internal int currentConveyorBeltID;


    private void Awake()
    {
        organs = GetComponentsInChildren<Organ>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
