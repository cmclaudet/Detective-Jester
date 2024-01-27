using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public string startNode;
    public bool shouldOneTimeTrigger = false;
    public UnityEvent onDialogueStart;
}
