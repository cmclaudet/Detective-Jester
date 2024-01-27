using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerDialogue : MonoBehaviour
{
    public DialogueRunner dr;

    public string startNode = "";
    private void Update()
    {
       if(Input.GetKeyDown("e") && startNode != "")
        {
            dr.StartDialogue(startNode);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter" +  other.name);
        if(other.GetComponent<DialogueManager>() != null)
        {
            startNode = other.GetComponent<DialogueManager>().startNode;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DialogueManager>() != null)
        {
            startNode = "";
        }
    }





}
