using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 1;
    Rigidbody body;

    void OnEnable()
    {
        JoyStick.joyMoved += JoyMoved;
    }

    private void OnDisable()
    {
        JoyStick.joyMoved -= JoyMoved;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void JoyMoved (Vector2 pos)
    {
        body.velocity = new Vector3(pos.x, 0, pos.y) * speed;
    }

}