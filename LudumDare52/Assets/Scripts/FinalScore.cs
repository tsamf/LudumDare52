using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Final Score: " + FindObjectOfType<GameManager>().getScore().ToString();
    }
}
