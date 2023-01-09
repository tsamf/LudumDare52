using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;


    private void UpdateScoreOnUI(float newScore)
    {
        if (textMesh)
        {
            textMesh.text = newScore.ToString("F2");
        }
    }


    private void OnEnable()
    {
        EventManager.OnGameScoreUpdatetHandler += UpdateScoreOnUI;
    }


    private void OnDisable()
    {
        EventManager.OnGameScoreUpdatetHandler -= UpdateScoreOnUI;
    }

    private void Awake()
    {
        Debug.Assert(textMesh != null, name + " is missing textMeshPro reference in the inspector");
    }
}
