using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = default;

    [Range(1, 3)]
    [SerializeField] private float minRateOfSpawning;
    [Range(3, 10)]
    [SerializeField] private float maxRateOfSpawning;


    [SerializeField] private float conveyorBeltSpeed;

    private float currentCBSpeed = 0;

    private void Awake()
    { 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void StartGame()
    {

    }

    private void EndGame()
    {

    }
}
