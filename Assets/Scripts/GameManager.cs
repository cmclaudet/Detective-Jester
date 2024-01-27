using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;


public class GameManager : MonoBehaviour
{
    public DialogueRunner dr;
    public PlayerInput pi;
    public ThirdPersonController tpc;
    private int lives = 2;
    private int laughts = 0;
    public List<CinemachineVirtualCamera> cams = new List<CinemachineVirtualCamera>();
    public Transform JestingPoint;
    public GameObject Player;




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
        Player.transform.position = JestingPoint.position;
        StartCoroutine(camSlide());
        tpc.MoveSpeed = 0;
        tpc.SprintSpeed = 0;
        //dr.StartDialogue("Phase2");
        
    }

    IEnumerator camSlide()
    {
        pi.DeactivateInput();
        cams[1].Priority = 100;
        yield return new WaitForSeconds(3f);
        cams[2].Priority = 200;
        yield return new WaitForSeconds(2f);
        dr.StartDialogue("Phase2");
        yield return null;
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
