using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityRandom = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Range(1,3)]
    [SerializeField] private float minRateOfSpawning;
    [Range(3, 10)]
    [SerializeField] private float maxRateOfSpawning;

    [SerializeField] private List<BodiesScriptableObject> bodies;
    [SerializeField] private GameObject body;

    [SerializeField] private ConveyorBelt[] conveyorBelts;

    private float timer;
    private float currentRateOfSpawning; // random b/w min and max rate of spawning

    public int randomBody;
    public int randomSpawnLocation;
    public List<int> availableConveyorBelts = new List<int>() { 0,1,2 };


    public void OnUpdateAvailableConveyorBelt(int id)
    {
        if (!availableConveyorBelts.Contains(id))
        {
            availableConveyorBelts.Add(id);
        }
    }



    public void SpawnBodies()
    {
        //Instantiate here;

        /// pick a conveyor belt to spawn

        if (availableConveyorBelts.Count <= 0)
        {
            return;
        }

        int randomIndexOfAvailableConveyorBelt = UnityRandom.Range(0, availableConveyorBelts.Count); /// pick random from available conveyor belts list
        int conveyorBeltIndx = availableConveyorBelts[randomIndexOfAvailableConveyorBelt]; /// coveyorbelt where the body will spawn
        availableConveyorBelts.RemoveAt(randomIndexOfAvailableConveyorBelt); /// will remove the select index from the list of available conveyor belt

        Transform spawnLocation = conveyorBelts[conveyorBeltIndx].bodySpawnLocation; /// spawnlocation

        GameObject go = Instantiate(body, spawnLocation.position, Quaternion.identity);

        /// fill body data

        randomBody = UnityRandom.Range(0, bodies.Count);
        if (go.TryGetComponent(out DeadBody deadBody))
        {
            BodiesScriptableObject bso = bodies[randomBody];
            deadBody.currentConveyorBeltID = conveyorBeltIndx;
            deadBody.bodyType = bso.bodyType;
            deadBody.bodySpriteRenderer.sprite = bso.bodySprite;
            deadBody.bodySpriteRenderer.color = bso.color;


            var t = bso.listOfOrgans.OrderBy(arg => Guid.NewGuid()).Take(2).ToList();

            OrganScriptableObject firstOrgan = t[0];
            OrganScriptableObject secondOrgan = t[1];

            deadBody.organOneSpriteRenderer.sprite  = firstOrgan.organSprite;
            deadBody.organOneSpriteRenderer.color   = firstOrgan.color; // for testing will be removed once we have the sprites

            deadBody.organTwoSpriteRenderer.sprite  = secondOrgan.organSprite;
            deadBody.organTwoSpriteRenderer.color   = secondOrgan.color; // for testing will be removed once we have the sprites
        }
    }

    private void Awake()
    {
        Debug.Assert(bodies != null, name + "Bodies scriptableObject reference is missing");
    }

    private void OnEnable()
    {
        EventManager.OnBodyGrindedEventHandler += OnUpdateAvailableConveyorBelt;
    }

    private void OnDisable()
    {
        EventManager.OnBodyGrindedEventHandler -= OnUpdateAvailableConveyorBelt;

    }



    // Start is called before the first frame update
    void Start()
    {
        RandomizeCurrentRateofSpawning();
    }

    private void RandomizeCurrentRateofSpawning()
    {
        currentRateOfSpawning = UnityRandom.Range(minRateOfSpawning, maxRateOfSpawning);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > currentRateOfSpawning)
        {
            RandomizeCurrentRateofSpawning();
            timer = 0f;
            SpawnBodies();
        }
    }

}
