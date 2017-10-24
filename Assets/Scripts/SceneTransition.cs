using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public float waitTime = 1;

    public string scene;

    private void Start()
    {
        StartCoroutine("InputDelay");
    }

    void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(waitTime);
        while (enabled)
        {

            if ((Input.GetButton("Green") || Input.GetButton("Green2")) && scene == "Menu Scene")
            {
                SceneManager.LoadScene(scene);
                AudioManager.instance.StartCoroutine("ChangeMusicBack");
                yield break;
            }
            else if ((Input.GetButton("Green") || Input.GetButton("Green2")) && scene == "Shootin Scene")
            {
                SceneManager.LoadScene(scene);
                AudioManager.instance.StartCoroutine("ChangeMusic");
                yield break;
            }
            else if (Input.GetButton("Red") || Input.GetButton("Red2"))
            {
                QuitGame();
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
