using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Yarn.Unity;

public class PlayerDialogue : MonoBehaviour {
    public ThirdPersonController ThirdPersonController;
    public DialogueRunner dr;

    public string startNode = "";


    private int interactions = 0;

    public GameObject ButtonPrompt;


    public void Awake()
    {
        dr.onDialogueStart.AddListener(() => ToggleLockCamera(true));
        dr.onDialogueComplete.AddListener(() => ToggleLockCamera(false));

        dr.AddCommandHandler<int>(
            "add_Interaction",    
            addInteraction 
        );
    }

    private void ToggleLockCamera(bool value) {
        ThirdPersonController.LockCameraPosition = value;
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
