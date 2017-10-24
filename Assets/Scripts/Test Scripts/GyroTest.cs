using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroTest : MonoBehaviour {

    public RawImage target;

    IEnumerator Start()
    {
        Input.gyro.enabled = true;
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamTexture tex = new WebCamTexture();
            target.texture = tex;
            tex.Play();
        }
    }

    void Update()
    {
        transform.rotation = Input.gyro.attitude;
        transform.Rotate(0, 0, 180, Space.Self);
        transform.Rotate(90, 180, 0, Space.World);
    }
}
