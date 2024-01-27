using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public string sceneName;
    private bool isLoading;

    public void Load() {
        if (isLoading == false) {
            SceneManager.LoadScene(sceneName);
            isLoading = true;
        }
    }
}
