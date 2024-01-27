using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public DialogueRunner dr;
    public PlayerInput pi;
    private int lives = 2;
    private int laughts = 0;


    private void Awake()
    {
        dr.AddCommandHandler<int>(
            "loseLife",
            loseLife
        );
        dr.AddCommandHandler<int>(
            "getLaught",
            getLaugh
        );
        dr.AddCommandHandler<int>(
            "startPhase2",
            phase2
        );
    }
    // Start is called before the first frame update
    void Start()
    {
        //Start Initial Dialogue
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void phase2(int x)
    {
        dr.Stop();
        pi.DeactivateInput();
        dr.StartDialogue("Phase2");
        
    }



    public void loseLife(int x)
    {
        lives -= x;
        if(lives <= 0)
        {
            badEnding();
        }
    }

    public void getLaugh(int x)
    {
        laughts += x;
        if(laughts >= 5)
        {
            goodEnding();
        }
    }

    private void badEnding()
    {
        dr.Stop();
        dr.StartDialogue("BadEnding");
    }

    private void goodEnding()
    {
        dr.Stop();
        dr.StartDialogue("GoodEnding");
    }
    
    
}
