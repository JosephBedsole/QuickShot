using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeCowboyController : MonoBehaviour {

    bool playerOneFired = false;
    bool playerTwoFired = false;

    public Animator playerOneAnim;
    public Animator playerTwoAnim;
    public Animator screenFlashAnim;

    Color color;

    int randNum;

    void Start()
    {
        randNum = Random.Range(0, 4);

        Debug.Log(randNum);
    }

    void Update()
    {
        IndicatorColor();
        Shoot();

        if (GameMananger.instance.readyState == true || GameMananger.instance.shootState == true)
        {
            if (   (Input.GetButtonDown("Green")  && randNum == 0) 
                || (Input.GetButtonDown("Red")    && randNum == 1) 
                || (Input.GetButtonDown("Blue")   && randNum == 2) 
                || (Input.GetButtonDown("Yellow") && randNum == 3))
            {
                playerOneFired = true;
            }
            else if ((Input.GetButtonDown("Green2")  && randNum == 0) 
                ||   (Input.GetButtonDown("Red2")    && randNum == 1) 
                ||   (Input.GetButtonDown("Blue2")   && randNum == 2) 
                ||   (Input.GetButtonDown("Yellow2") && randNum == 3))
            {
                Debug.Log("Player2 is Firing");
                playerTwoFired = true;
            }
            else return;
        }
    }

    void IndicatorColor()
    {
        Debug.Log(randNum);

        switch (randNum)
        {
            case 0:
                color = Color.green;
                break;
            case 1:
                color = Color.red;
                break;
            case 2:
                color = Color.blue;
                break;
            default:
                Debug.Log("I'm yellow!");
                color = Color.yellow;
                break;
        }

        GameMananger.instance.shootIndicator.gameObject.GetComponent<Image>().color = color;
    }

    public void Shoot()
    {
        if (playerOneFired)
        {
            Debug.Log("Player One Shot");

            if (GameMananger.instance.readyState == true)
            {
                GameMananger.instance.playerTwoWins.SetActive(true);
                GameMananger.instance.falseStart.SetActive(true);
                GameMananger.instance.playerTwoScore += 1;

                GameMananger.instance.readyState = false;
                GameMananger.instance.shootState = false;

                GameStateManager.instance.StopAllCoroutines();
            }
            else if (GameMananger.instance.shootState == true)
            {
                playerOneAnim.SetTrigger("Shoot");
                screenFlashAnim.SetTrigger("Flash");
                playerTwoAnim.SetTrigger("Death");

                GameMananger.instance.shootIndicator.SetActive(false);
                GameMananger.instance.playerOneWins.SetActive(true);
                GameMananger.instance.playerOneScore += 1;

                GameMananger.instance.readyState = false;
                GameMananger.instance.shootState = false;

                AudioManager.PlayVariedEffect("RichocheShot");

                GameStateManager.instance.StopAllCoroutines();
            }
        }
        else if (playerTwoFired)
        {
            Debug.Log("Player Two Shot");

            if (GameMananger.instance.readyState == true)
            {
                GameMananger.instance.playerOneWins.SetActive(true);
                GameMananger.instance.falseStart.SetActive(true);
                GameMananger.instance.playerOneScore += 1;

                GameMananger.instance.readyState = false;
                GameMananger.instance.shootState = false;

                GameStateManager.instance.StopAllCoroutines();
            }
            else if (GameMananger.instance.shootState == true)
            {
                playerTwoAnim.SetTrigger("Shoot");
                screenFlashAnim.SetTrigger("Flash");
                playerOneAnim.SetTrigger("Death");

                Debug.Log("I'm shooting stuff");

                GameMananger.instance.shootIndicator.SetActive(false);
                GameMananger.instance.playerTwoWins.SetActive(true);
                GameMananger.instance.playerTwoScore += 1;

                GameMananger.instance.readyState = false;
                GameMananger.instance.shootState = false;

                AudioManager.PlayVariedEffect("RichocheShot");

                GameStateManager.instance.StopAllCoroutines();
            }
        }
    }

}
