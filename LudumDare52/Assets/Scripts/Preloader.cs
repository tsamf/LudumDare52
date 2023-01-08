using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    [SerializeField] private GameObject[] preLoadObjects;

    private void Awake()
    {
        if (preLoadObjects != null && preLoadObjects.Length > 0)
        {
            for (int index = 0; index < preLoadObjects.Length; index++)
            {
                GameObject go = Instantiate(preLoadObjects[index], Vector3.zero, Quaternion.identity);
            }
        }
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
