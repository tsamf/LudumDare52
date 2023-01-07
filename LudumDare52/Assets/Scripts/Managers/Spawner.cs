using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{

    [SerializeField] private float rateOfSpawning;
    [SerializeField] private int cBeltID;
    [SerializeField] private List<BodiesScriptableObject> bodies;

    private float timer;

    public void SpawnBodies()
    {
        //Instantiate here;
    }

    private void Awake()
    {
        Debug.Assert(bodies != null, name + "Bodies scriptableObject reference is missing");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > rateOfSpawning)
        {
            timer = 0f;
            SpawnBodies();
        }
    }

}
