using UnityEngine;

namespace DefaultNamespace {
  public class StartPhase2DialogueManager : DialogueManager {
    [SerializeField] private string phase2StartNode;

    public void ActivatePhase2StartNode() {
      base.activeStartNodes = new[] { phase2StartNode };
    }
  }
}