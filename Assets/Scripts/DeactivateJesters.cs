using UnityEngine;

namespace DefaultNamespace {
  public class DeactivateJesters : MonoBehaviour {
    public GameObject[] Jesters;

    public void DeactivateJester(int dialoguesRead) {
      if (dialoguesRead <= Jesters.Length - 1) {
        Jesters[dialoguesRead - 1].SetActive(false);
      }
    }

    public void DeactivateAllJesters() {
      foreach (var jester in Jesters) {
        jester.SetActive(false);
      }
    }
  }
}