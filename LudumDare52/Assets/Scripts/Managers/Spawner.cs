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

    private int randomBody;
    private List<int> availableConveyorBelts = new List<int>() { 0,1,2 };

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
            //deadBody.bodySpriteRenderer.color = bso.color;

            int noOfOrgansToSpawn = UnityRandom.Range(1,3);

            var t = bso.listOfOrgans.OrderBy(arg => Guid.NewGuid()).Take(noOfOrgansToSpawn).ToList();

            OrganScriptableObject firstOrgan = t[0];

            Organ organOne = deadBody.organs[0];

            organOne.gameObject.SetActive(true);
            organOne.scriptableObject       = firstOrgan;
            organOne.spriteRenderer.sprite  = firstOrgan.organSprite;
            organOne.startScore             = firstOrgan.score;
            organOne.decayRate              = firstOrgan.decayRate;
            organOne.toolToUse              = firstOrgan.toolToUse;

            if (noOfOrgansToSpawn > 1)
            {
                OrganScriptableObject secondOrgan = t[1];

                Organ organTwo = deadBody.organs[1];

                organTwo.gameObject.SetActive(true);
                organTwo.scriptableObject       = secondOrgan;
                organTwo.spriteRenderer.sprite  = secondOrgan.organSprite;
                organTwo.startScore             = secondOrgan.score;
                organTwo.decayRate              = secondOrgan.decayRate;
                organTwo.toolToUse              = secondOrgan.toolToUse;
            }
            //organTwo.spriteRenderer.color   = secondOrgan.color;        // for testing will be removed once we have the sprites

            // obsolete
            //deadBody.organTwoSpriteRenderer.sprite  = secondOrgan.organSprite;
            //deadBody.organTwoSpriteRenderer.color   = secondOrgan.color; // for testing will be removed once we have the sprites
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
        if (!GameManager.instance.gameState.Equals(LDEnums.GameState.Running))
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > currentRateOfSpawning)
        {
            RandomizeCurrentRateofSpawning();
            timer = 0f;
            SpawnBodies();
        }
    }



}
