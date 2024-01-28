using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;
using Cinemachine;
using DefaultNamespace;
using StarterAssets;


public class GameManager : MonoBehaviour
{
    public DialogueRunner dr;
    public PlayerInput pi;
    public ThirdPersonController tpc;
    [SerializeField] private int lives = 2;
    private int laughts = 0;
    [SerializeField] private int dialoguesReadPhase2Threshold;
    [SerializeField] private int uniqueDialoguesReadPhase2Threshold;
    [SerializeField] private StartPhase2DialogueManager startPhase2DialogueManager;
    [SerializeField] private DeactivateJesters deactivateJesters;
    [SerializeField] private SoundManager SoundManager;
    
    public List<CinemachineVirtualCamera> cams = new List<CinemachineVirtualCamera>();
    public Transform JestingPoint;
    public GameObject Player;
    
    private readonly List<string> fullyReadDialoguesByName = new ();
    private int totalDialogueInteractionsCount;
    public bool isPhase2Started;

    //Ending Logic
    bool finale = false;
    public float cameraCutoff = -18;
    public GameObject boulder;
    public float boulderYOffset = 4;


    private void Awake() {
        dr.AddCommandHandler<int>(
            "loseLife",
            loseLife
        );
        dr.AddCommandHandler<int>(
            "getLaugh",
            getLaugh
        );
        dr.AddCommandHandler<int>(
            "startPhase2",
            phase2
        );
        dr.AddCommandHandler<bool>(
            "triggerEnding",
            ending
            );
        dr.AddCommandHandler<bool>(
            "allowMovement",
            allowMovement
            );
        dr.AddCommandHandler<string>(
            "playSound",
            playSound
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
        if (finale)
        {
            if (Player.transform.position.z < cameraCutoff)
            {
                tpc.MoveSpeed = 0;
                tpc.SprintSpeed = 0;
                dr.StartDialogue("LastWords");

            }
        }
    }

    public void phase2(int x) {
        isPhase2Started = true;
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
        if(laughts >= 4)
        {
            finalQuestion();
        }
    }

    public void playSound(string audioId)
    {
        SoundManager.playSound(audioId);
    }

    public void TryAddReadDialogue(string name) {
        if (fullyReadDialoguesByName.Contains(name) == false)
        {
            fullyReadDialoguesByName.Add(name);
            deactivateJesters.DeactivateJester(fullyReadDialoguesByName.Count);
        }
        IncrementDialogueInteractions();
        CheckPhase2Unlock();
    }

    public void IncrementDialogueInteractions() {
        totalDialogueInteractionsCount++;
        CheckPhase2Unlock();
    }

    private void CheckPhase2Unlock() {
        if (totalDialogueInteractionsCount >= dialoguesReadPhase2Threshold ||
            fullyReadDialoguesByName.Count >= (uniqueDialoguesReadPhase2Threshold)) {
            startPhase2DialogueManager.ActivatePhase2StartNode();
            deactivateJesters.DeactivateAllJesters();
        }
    }

    private void badEnding()
    {
        dr.Stop();
        dr.StartDialogue("BadEnding");
    }

    private void finalQuestion()
    {
        dr.Stop();
        dr.StartDialogue("FinalQuestion");
    }
    private void ending(bool good)
    {
        if (good)
        {
            tpc.MoveSpeed =  2;
            tpc.SprintSpeed = 5.3f;
            pi.ActivateInput();
            finale = true;

        }
        else
        {
            Vector3 boulderPos = new Vector3(JestingPoint.position.x, JestingPoint.position.y + boulderYOffset, JestingPoint.position.z);
            Instantiate(boulder, boulderPos, Quaternion.identity);
            dr.Stop();
            dr.StartDialogue("Death");
            Player.SetActive(false);
            
            //Death
        }
    }

    public void allowMovement(bool b)
    {
        pi.ActivateInput();
        tpc.MoveSpeed = 2;
        tpc.SprintSpeed = 5.3f;
    }
    
}
