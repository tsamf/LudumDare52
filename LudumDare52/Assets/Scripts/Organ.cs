using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Organ : MonoBehaviour
{
    public Image fillImage;

    [Header("Set Dynamically")]
    public Gradient gradiant;

    [SerializeField] internal OrganScriptableObject scriptableObject;
    [SerializeField] internal SpriteRenderer spriteRenderer = default;

    [SerializeField] internal bool isDecomposed = false; /// use this to prevent player from harvest organ which are decomposed
    [SerializeField] internal bool isHarvested = false;

    [Tooltip("value 60 means in 1 second this organ will be decomposed")]
    [SerializeField] internal float decayRate;
    [SerializeField] internal float startScore; // inital score

    [SerializeField] internal LDEnums.Tools toolToUse;

    [SerializeField] private Slider slider = default;

    private float startLife = 1; /// inital lifeSpan

    [SerializeField] private float currentLife = 0;
    private float currentScore = default;

    internal void OnOrganPickedUP()
    {
        isHarvested = true;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        slider = GetComponentInChildren<Slider>(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        currentLife = startLife;
        currentScore = startScore;
    }
  
    // Update is called once per frame
    void Update()
    {
        currentLife -= (decayRate/60 * Time.deltaTime);
        currentScore = startScore * currentLife; /// percentage based on the start score
        //slider.value = currentLife;
        fillImage.color = gradiant.Evaluate(currentLife);
        if (currentLife >= 0)
        {
            ///
            isDecomposed = true; //
            currentScore = 0;
        }
    }
}
