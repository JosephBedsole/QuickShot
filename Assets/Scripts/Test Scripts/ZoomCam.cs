using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour {
    // Note: ZOOM | PAN | ROTATE

    public float zoomDist = 1;
    public float panDist = -10;

    float prevAng;
    float prevDist;
    Vector2 previousPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount < 2) return;

        Touch a = Input.GetTouch(0);
        Touch b = Input.GetTouch(1);

        if (b.phase == TouchPhase.Began)
        {
            Vector2 diff = b.position - a.position;
            prevAng = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            prevDist = diff.magnitude;
            previousPos = (a.position + b.position) * 0.5f;
        }
        else if (a.phase == TouchPhase.Moved || b.phase == TouchPhase.Moved)
        {
            Vector2 diff = b.position - a.position;
            float dist = Vector3.Distance(a.position, b.position);
            Zoom(dist - prevDist);
            prevDist = dist;

            Vector2 pos = (a.position + b.position) * 0.5f;
            Pan(pos - previousPos);
            previousPos = pos;

            float ang = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Rotate(ang - prevAng);
            prevAng = ang;

        }
    }

    void Zoom(float change)
    {
        float frac = change / Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
        transform.position += transform.forward * frac * zoomDist;
    }

    void Pan (Vector2 change)
    {
        change.x /= Screen.width;
        change.y /= Screen.height;
        transform.position += (transform.up * change.y + transform.right * change.x) * panDist;
    }

    void Rotate (float change)
    {
        transform.Rotate(Vector3.back * change);

    }
}
