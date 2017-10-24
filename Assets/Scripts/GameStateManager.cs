using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager instance;
    float waitTime;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine("StateChanger");

        waitTime = Random.Range(1, 10);
        Debug.Log(waitTime);
    }

    IEnumerator StateChanger ()
    {
        yield return new WaitForSeconds(3);
        GameMananger.instance.readyState = true;
        GameMananger.instance.getReady.SetActive(true);
        yield return new WaitForSeconds(1);
        GameMananger.instance.getReady.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        GameMananger.instance.readyState = false;
        GameMananger.instance.shootState = true;
        GameMananger.instance.shootIndicator.SetActive(true);
        AudioManager.PlayEffect("smack");
        yield return new WaitForSeconds(0.5f);
        GameMananger.instance.shootIndicator.SetActive(false);
    }
}
