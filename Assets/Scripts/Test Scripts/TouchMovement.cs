 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour {


    Dictionary<int, GameObject> touchedObjects = new Dictionary<int, GameObject>();
    Dictionary<int, Vector3> touchVelocities = new Dictionary<int, Vector3>();

    // int i = Input.touchCount - 1;

    void Update ()
    {
        if (Input.touchCount == 0) return;

        // replace
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Touch t = Input.GetTouch(i);
            UpdateTouch(t);
        }
	}

    void UpdateTouch (Touch t)
    {
        if (t.phase == TouchPhase.Began)
        {
            Ray r = Camera.main.ScreenPointToRay(t.position);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit) && hit.collider.gameObject.CompareTag("Cube")) // Start using .CompareTag("Thing") instead of .tag == "Thing"; Amen!
            {
                touchedObjects[t.fingerId] = hit.collider.gameObject;
                touchVelocities[t.fingerId] = hit.collider.GetComponent<Rigidbody>().velocity;
            }
        }
        else if (touchedObjects.ContainsKey(t.fingerId) && (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary))
        {
            GameObject touchedObj = touchedObjects[t.fingerId];
            Vector3 touchPos = t.position;
            touchPos.z = 4; // touchedObj.transform.position.z - Camera.main.transform.position.z;
            Vector3 oldPos = touchedObj.transform.position;
            touchedObj.transform.position = Camera.main.ScreenToWorldPoint(touchPos);
            Vector3 vel = (touchedObj.transform.position - oldPos) / Time.deltaTime;
            touchVelocities[t.fingerId] = (touchVelocities[t.fingerId] + vel) * 0.5f;
        }
        else if (touchedObjects.ContainsKey(t.fingerId) && (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled))
        {
            Vector3 v = touchVelocities[t.fingerId];
            float m = v.magnitude;
            v.z = m * 0.5f;
            v = v.normalized * m;
            // touchedObjects[t.fingerId].GetComponent<Rigidbody>().velocity = touchVelocities[t.fingerId];
            touchedObjects[t.fingerId].GetComponent<Rigidbody>().velocity = v;
            touchVelocities.Remove(t.fingerId);
            touchedObjects.Remove(t.fingerId);
        }
    }
}
