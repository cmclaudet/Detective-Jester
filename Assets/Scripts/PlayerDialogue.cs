using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerDialogue : MonoBehaviour {
    public PlayerInput PlayerInput;
    public ThirdPersonController ThirdPersonController;
    public DialogueRunner dr;

    string startNode = "";
    UnityEvent onDialogueStart;


    private int interactions = 0;

    public GameObject ButtonPrompt;


    public void Awake()
    {
        dr.onDialogueStart.AddListener(() => ToggleLockCamera(true));
        dr.onDialogueComplete.AddListener(() => {
                                              ToggleLockCamera(false);
                                              PlayerInput.ActivateInput();
                                          });

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
            StartDialogue();
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
        DialogueManager dialogueManager = other.GetComponent<DialogueManager>();
        if(dialogueManager != null)
        {
            startNode = dialogueManager.startNode;
            if (dialogueManager.onDialogueStart != null) {
                onDialogueStart = dialogueManager.onDialogueStart;
            }
            if (dialogueManager.shouldOneTimeTrigger) {
                StartDialogue();
                PlayerInput.DeactivateInput();
                dialogueManager.shouldOneTimeTrigger = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DialogueManager>() != null)
        {
            ButtonPrompt.SetActive(false);
            startNode = "";
            onDialogueStart = null;
            StopDialogue();
        }
    }

    private void StartDialogue() {
        if (onDialogueStart != null) {
            onDialogueStart.Invoke();
        }
        dr.StartDialogue(startNode);
    }

    private void StopDialogue() {
        dr.Stop();
        PlayerInput.ActivateInput();
    }
   
    



}
