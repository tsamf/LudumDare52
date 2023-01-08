using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class HarvestBar : MonoBehaviour
{
    [Header("Set In Inspector")]

    public Image fillImage;

    [Header("Set Dynamically")]
    public Gradient gradiant;
    public Canvas canvas;


    public void EnableBar()
    {
        if (canvas != null)
        {
            canvas.enabled = true;
        }
    }

    public void DisableBar()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }

    private void Awake()
    {
        Debug.Assert(fillImage != null, name + " is missing fill image reference in the inspector");
        canvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    internal void UpdateBar(float value)
    {
        fillImage.color = gradiant.Evaluate(value);
    }
}
