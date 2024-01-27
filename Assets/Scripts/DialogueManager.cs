using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    // public string startNode;
    public string[] startNodes;
    public bool shouldOneTimeTrigger = false;
    public UnityEvent onDialogueStart;

    public int startNodeIndex { get; private set; }

    public void IncrementNodeIndex() {
        if (startNodeIndex >= startNodes.Length - 1) {
            startNodeIndex = 0;
        } else {
            startNodeIndex += 1;
        }
    }
}
