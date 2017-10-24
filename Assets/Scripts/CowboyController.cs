using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyController : MonoBehaviour {

    float halfScreenWidth;

    bool playerOneFired = false;
    bool playerTwoFired = false;

    public Animator playerOneAnim;
    public Animator playerTwoAnim;
    public Animator screenFlashAnim;

	void Start ()
    {
        halfScreenWidth = Screen.width * 0.5f;
	}

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        if (t.position.x < halfScreenWidth && !playerTwoFired && (GameMananger.instance.readyState == true || GameMananger.instance.shootState == true))
        {
            Debug.Log("TOUCHED LEFT!");
            playerOneFired = true;
        }
        else if (t.position.x > halfScreenWidth && !playerOneFired && (GameMananger.instance.readyState == true || GameMananger.instance.shootState == true))
        {
            Debug.Log("TOUCHED RIGHT");
            playerTwoFired = true;
        }

        Shoot();

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
