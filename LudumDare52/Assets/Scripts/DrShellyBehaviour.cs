using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrShellyDialogue : MonoBehaviour
{
    [Serializable]
    public class Dialogue 
    {
        public int id;
        public string msg;
    }

    public List<Dialogue> behaviour = new List<Dialogue>();
    public TextMeshProUGUI tmpUI;

    public void PerformBehaviour(int id)
    {
        int indx = behaviour.FindIndex(value => value.id == id);
        if (indx > -1)
        {
            tmpUI.text = behaviour[indx].msg;
            Debug.LogFormat("msg >> ",behaviour[indx].msg);
        }
    }

    public int testBehaviourIndx = -1;
    public int lastTestBehaviourIndx = -1;

    public void Update()
    {
        if (lastTestBehaviourIndx != testBehaviourIndx)
        {
            lastTestBehaviourIndx = testBehaviourIndx;
            PerformBehaviour(testBehaviourIndx);
        }
    }
}
