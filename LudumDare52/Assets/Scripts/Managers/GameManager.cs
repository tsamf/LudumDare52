using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Range(1, 3)]
    [SerializeField] private float minRateOfSpawning;

    [Range(3, 10)]
    [SerializeField] private float maxRateOfSpawning;

    [SerializeField] internal float conveyorBeltSpeed = 2;

    [SerializeField] internal int maxNoOfOrgansGrindingAllowed = 10;

    [SerializeField] internal float maxRageBarValue = 1;

    [SerializeField] internal LDEnums.GameState gameState = LDEnums.GameState.None;

    [SerializeField] internal float currentScore = 0;

    private float currentCBSpeed = 0;

    [SerializeField] internal List<ScoreData> organsCollected = new List<ScoreData>();

    [SerializeField] AudioClip deathSFX;
    [SerializeField] float timeToLoadScoreScene = 1f;

    private void Awake()
    { 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventManager.OnGameOverEventHandler += EndGame;
        EventManager.OnPlayerCollectOrganEventHandler += OnOrganCollectedEvent;
    }

    private void OnDisable()
    {
        EventManager.OnGameOverEventHandler -= EndGame;
        EventManager.OnPlayerCollectOrganEventHandler -= OnOrganCollectedEvent;

    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = LDEnums.GameState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCBSpeed != conveyorBeltSpeed)
        {
            currentCBSpeed = conveyorBeltSpeed;
            EventManager.RaiseOnCBUpdateSpeedEvent(currentCBSpeed);
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene(1);
    }

    private void StartGame()
    {

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

    public float getScore()
    {
        return currentScore; 
    }
}
