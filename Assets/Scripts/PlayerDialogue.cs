using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerDialogue : MonoBehaviour
{
    public DialogueRunner dr;

    public string startNode = "";


    private int interactions = 0;

    public GameObject ButtonPrompt;


    public void Awake()
    {
        dr.AddCommandHandler<int>(
            "add_Interaction",    
            addInteraction 
        );

        
    }


    private void addInteraction(int dir)
    {
        interactions += dir;
        Debug.Log(interactions);
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
        ButtonPrompt.SetActive(true);
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
            ButtonPrompt.SetActive(false);
            startNode = "";
            dr.Stop();
        }
    }

    


   
    



}
