using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerDialogue : MonoBehaviour
{
    public DialogueRunner dr;

    public string startNode = "";


    private int interactions = 0;


    public void Awake()
    {
        dr.AddCommandHandler<GameObject>(
            "add_Interaction",    
            addInteraction 
        );

        
    }


    private void addInteraction(GameObject target)
    {
        interactions += 1;
    }

    private int getInteraction(GameObject target)
    {
        return interactions;
    }




    private void Update()
    {
       if(Input.GetKeyDown("e") && startNode != "" && !dr.IsDialogueRunning)
        {
            dr.StartDialogue(startNode);
        }
       if(Input.GetKeyDown("r") && dr.IsDialogueRunning)
        {
            dr.Stop();
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
            dr.Stop();
        }
    }

    


   
    



}
