using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public string sceneName;
    private bool isLoading;
    public Animator anim;

    public void Load() {
        if (isLoading == false) {
            isLoading = true;
            StartCoroutine(StartScene());
        }
    }

    IEnumerator StartScene()
    {
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        yield return null;
    }

}
