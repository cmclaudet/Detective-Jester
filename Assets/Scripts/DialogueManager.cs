using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    // public string startNode;
    [SerializeField] string[] startNodes;
    [HideInInspector]public string[] activeStartNodes;
    public bool shouldOneTimeTrigger = false;
    public bool shouldIncrementDialogueInteractions = true;
    public UnityEvent onDialogueStart;

    public int startNodeIndex { get; protected set; }
    public bool isSeen { get; protected set; }

    private void Awake() {
        SetStartNodes();
    }

    private void SetStartNodes() {
        activeStartNodes = startNodes;
    }

    public void IncrementNodeIndex() {
        if (startNodeIndex >= activeStartNodes.Length - 1) {
            startNodeIndex = 0;
            isSeen = true;
        } else {
            startNodeIndex += 1;
        }
    }
}
