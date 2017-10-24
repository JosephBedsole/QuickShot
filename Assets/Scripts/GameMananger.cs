using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMananger : MonoBehaviour {

    public static GameMananger instance;

    [Header("Scores")]
    public int playerOneScore;
    public int playerTwoScore;
    public Text playerOneTextScore;
    public Text playerTwoTextScore;

    [Header("UI Events")]
    public GameObject getReady;
    public GameObject shootIndicator;
    public GameObject playerOneWins;
    public GameObject playerTwoWins;
    public GameObject falseStart;

    public bool readyState = false;
    public bool shootState = false;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start ()
    {
        AudioManager.PlayMusic();
        Cursor.visible = false;
	}
	
	void Update ()
    {
        playerOneTextScore.text = "" + playerOneScore;

        playerTwoTextScore.text = "" + playerTwoScore;
	}
}
