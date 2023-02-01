using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using UnityRandom = UnityEngine.Random;


[Serializable]
public struct ScoreData
{
    public LDEnums.OrgansType organType;
    public float score;

    public ScoreData(LDEnums.OrgansType organType, float score)
    {
        this.organType = organType;
        this.score = score;
    }
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance = default;

    [SerializeField] internal int maxNoOfOrgansGrindingAllowed = 10;

    [SerializeField] internal float maxRageBarValue = 1;

    [SerializeField] private LDEnums.GameState startState = LDEnums.GameState.StartMenu;

    [SerializeField] internal LDEnums.GameState gameState = LDEnums.GameState.None;

    [SerializeField] internal float currentScore = 0;

    [SerializeField] internal List<ScoreData> organsCollected = new List<ScoreData>();

    [SerializeField] AudioClip deathSFX;
    [SerializeField] float timeToLoadScoreScene = 1f;

    [Header("Conveyor Belt")]

    [SerializeField] private float minConveyorBeltSpeed;
    [SerializeField] private float maxConveyorBeltSpeed;

    [SerializeField] private float currentConveyorBeltSpeed = 2;

    private float lastConveyorBeltSpeed = 0;

    [Header("Repairables")]

    [SerializeField] private float minTimeBtwnBreakdowns;
    [SerializeField] private float maxTimeBtwnBreakdowns;

    [SerializeField] private List<LDEnums.RepairableObjects> availableRepairables;

    private bool  canObjectBreakdown = false;
    [SerializeField] private float currentTimeBtwnBreakdowns     = 0;
    private float objectsBreakdownEventTimer    = 0;

    [SerializeField] internal InputActionReference pauseResumeInputAction;

    public static float CurrentConveyorBeltSpeed 
    {
        get
        {
            if (instance) 
                return instance.currentConveyorBeltSpeed;
            return 0;
        }
    }

    public float getScore()
    {
        return currentScore;
    }

    public void UpdateGameState(LDEnums.GameState newState)
    {
        gameState = newState;
        Debug.LogFormat("Update game state to (0)", newState);
    }

    #region Mono

    private void Awake()
    { 
        instance = this;
        DontDestroyOnLoad(gameObject);

        currentTimeBtwnBreakdowns   = GetRandomTimeBtwnBreakdowns();
        currentConveyorBeltSpeed    = GetRandomConveyorBeltSpeed();

        availableRepairables = new List<LDEnums.RepairableObjects>()
        {
            LDEnums.RepairableObjects.AC
            //LDEnums.RepairableObjects.FuseBox
        };
    }

    private void OnEnable()
    {
        pauseResumeInputAction                          = InputManager.instance.pauseResume;
        EventManager.OnGameOverEventHandler             += EndGame;
        EventManager.OnPlayerCollectOrganEventHandler   += OnOrganCollectedEvent;
        EventManager.OnObjectRepairedEventHandler       += OnObjectRepairedEvent;
    }

    private void OnDisable()
    {
        pauseResumeInputAction                          = null;
        EventManager.OnGameOverEventHandler             -= EndGame;
        EventManager.OnPlayerCollectOrganEventHandler   -= OnOrganCollectedEvent;
        EventManager.OnObjectRepairedEventHandler       -= OnObjectRepairedEvent;
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameState = LDEnums.GameState.Running;
        UpdateGameState(startState);
        canObjectBreakdown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastConveyorBeltSpeed != currentConveyorBeltSpeed)
        {
            lastConveyorBeltSpeed = currentConveyorBeltSpeed;
            //Debug.LogFormat("gm change speed {0}", currentCBSpeed);

            EventManager.RaiseOnCBUpdateSpeedEvent(currentConveyorBeltSpeed);
        }

        if (gameState.Equals(LDEnums.GameState.Running) && availableRepairables.Count >= 0 && canObjectBreakdown)
        {
            objectsBreakdownEventTimer += Time.deltaTime;
            if (objectsBreakdownEventTimer >= currentTimeBtwnBreakdowns)
            {
                currentTimeBtwnBreakdowns   = GetRandomTimeBtwnBreakdowns();
                objectsBreakdownEventTimer  = 0f;
                canObjectBreakdown          = false;
                InitiateObjectBreakdown();
            }
        }

        if (!gameState.Equals(LDEnums.GameState.StartMenu) && pauseResumeInputAction.action != null && pauseResumeInputAction.action.WasPressedThisFrame())
        {
            LDEnums.GameState changeStateTo = !gameState.Equals(LDEnums.GameState.Paused) ? LDEnums.GameState.Paused: LDEnums.GameState.Running;
            UpdateGameState(changeStateTo);
        }
    }

    #endregion


    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        LoadNextScene();
        UpdateGameState(LDEnums.GameState.Running);
    }

    private void EndGame()
    {
        Debug.LogFormat("Game Over");
        gameState = LDEnums.GameState.Over;
        StartCoroutine(death());
    }

    IEnumerator death()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
        yield return new WaitForSeconds(timeToLoadScoreScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnOrganCollectedEvent(LDEnums.OrgansType organ, float score)
    {
        organsCollected.Add(new ScoreData(organ, score));
        currentScore += score;
        //Debug.LogFormat("Score updated {0}", currentScore);

        EventManager.RaiseGameScoreUpdateEvent(currentScore);
    }

    private float GetRandomConveyorBeltSpeed()
    {
        return UnityRandom.Range(minConveyorBeltSpeed, maxConveyorBeltSpeed);
    }


    #region Repairable

    private float GetRandomTimeBtwnBreakdowns()
    {
        return UnityRandom.Range(minTimeBtwnBreakdowns, maxTimeBtwnBreakdowns);
    }

    private void InitiateObjectBreakdown()
    {
        if (availableRepairables.Count <= 0)
            return;

        /// pick an object to break from available objects
        int selectedIndx = UnityRandom.Range(0, availableRepairables.Count);

        LDEnums.RepairableObjects breakdownObject = availableRepairables[selectedIndx];
        availableRepairables.Remove(breakdownObject);

        EventManager.RaiseOnObjectBreakdownEvent(breakdownObject);
    }

    private void OnObjectRepairedEvent(LDEnums.RepairableObjects repairedObject)
    {
        canObjectBreakdown = true;
        if (!availableRepairables.Contains(repairedObject))
        {
            availableRepairables.Add(repairedObject);
        }
    }

    #endregion

}
