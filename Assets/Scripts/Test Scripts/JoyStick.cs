using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public delegate void JoyStickMoved(Vector2 pos);
    public static event JoyStickMoved joyMoved = delegate{};

    public Transform root;
    public SpriteRenderer joySprite;
    public float maxDist = 0.05f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        root.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
                TouchStart(t.position);
                Moved(t.position);
        }
        else if (t.phase == TouchPhase.Moved)
        {
            Moved(t.position);
        }
        else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
        {
            joyMoved(Vector2.zero);
            joySprite.transform.localPosition = Vector3.zero;
            root.gameObject.SetActive(false);
        }
    }

    void TouchStart (Vector3 screenPos)
    {

        root.gameObject.SetActive(true);
        screenPos.z = -root.position.y + cam.transform.position.y;
        root.position = cam.ScreenToWorldPoint(screenPos);

    }

    void Moved (Vector3 screenPos)
    {
        screenPos.z = -joySprite.transform.position.y + cam.transform.position.y;
        joySprite.transform.position = cam.ScreenToWorldPoint(screenPos);
        if (joySprite.transform.localPosition.magnitude > maxDist)
        {
            joySprite.transform.localPosition = joySprite.transform.localPosition.normalized * maxDist;
        }

        Vector2 pos = joySprite.transform.localPosition;
        joyMoved(pos);

    }
}
