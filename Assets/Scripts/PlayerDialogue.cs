using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Yarn.Unity;
using DefaultNamespace;

public class PlayerDialogue : MonoBehaviour {
    public PlayerInput PlayerInput;
    public ThirdPersonController ThirdPersonController;
    public DialogueRunner dr;
    public GameManager GameManager;

    private DialogueManager activeDialogueManager;

    private int interactions = 0;

    public GameObject ButtonPrompt;


    public void Awake()
    {
        dr.onDialogueStart.AddListener(() => ToggleLockCamera(true));
        dr.onDialogueComplete.AddListener(() => {
                                              ToggleLockCamera(false);
                                              PlayerInput.ActivateInput();
                                              TryActivateButtonPrompt();
                                          });

        dr.AddCommandHandler<int>(
            "add_Interaction",    
            addInteraction 
        );
    }

    private void ToggleLockCamera(bool value) {
        ThirdPersonController.LockCameraPosition = value;
        if (value)
        {
            PlayerInput.DeactivateInput();
        }
        else {
            PlayerInput.ActivateInput();
        }
        
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
       if(Input.GetKeyDown("e") && activeDialogueManager != null && !dr.IsDialogueRunning)
        {
            StartDialogue();
            ButtonPrompt.SetActive(false);
        }
       if(Input.GetKeyDown("r") && dr.IsDialogueRunning)
        {
            dr.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TryActivateButtonPrompt();
        Debug.Log("Enter" +  other.name);
        DialogueManager dialogueManager = other.GetComponent<DialogueManager>();
        if(dialogueManager != null) {
            Debug.Log("enter dialogue trigger");
            activeDialogueManager = dialogueManager;
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
            activeDialogueManager = null;
            PlayerInput.ActivateInput();
        }
    }

    private void StartDialogue() {
        if (activeDialogueManager.onDialogueStart != null) {
            activeDialogueManager.onDialogueStart.Invoke();
        }
        dr.StartDialogue(activeDialogueManager.activeStartNodes[activeDialogueManager.startNodeIndex]);
        activeDialogueManager.IncrementNodeIndex();

        if (activeDialogueManager.shouldIncrementDialogueInteractions) {
            if (activeDialogueManager.isSeen) {
                GameManager.TryAddReadDialogue(activeDialogueManager.gameObject.name);
            } else {
                GameManager.IncrementDialogueInteractions();
            }
        }
    }

    void TryActivateButtonPrompt() {
        if (GameManager.isPhase2Started) {
            return;
        }
        ButtonPrompt.SetActive(true);
    }


}
