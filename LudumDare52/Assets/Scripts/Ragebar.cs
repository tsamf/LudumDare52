using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Ragebar : MonoBehaviour
{
    [Header("Set In Inspector")]

    public Slider slider;

    [Header("Set Dynamically")]
    public Gradient gradiant;

    public float value;
    public bool testValue = false; // for testing only

    private void Awake()
    {
        slider = GetComponent<Slider>();
        Debug.Assert(slider != null, name + " is missing fill image reference in the inspector");
        slider.maxValue = GameManager.instance.maxRageBarValue;
    }

    private void OnEnable()
    {
        EventManager.OnUpdateRageMeterEventHandler += UpdateBar;
    }

    private void OnDisable()
    {
        EventManager.OnUpdateRageMeterEventHandler -= UpdateBar;
    }

    // Update is called once per frame
    internal void UpdateBar(float value)
    {
        //Debug.LogFormat("rage value {0} ", value);

        value /= slider.maxValue;
        slider.value += value;
        ColorBlock colorBlock = slider.colors; 
        colorBlock.normalColor = gradiant.Evaluate(slider.value);
        slider.colors = colorBlock;

        if (slider.value.Equals(slider.maxValue))
        {
            EventManager.RaiseGameOverEvent();
        }
    }

    private void Update()
    {
       if(testValue) UpdateBar(value);
    }
}

