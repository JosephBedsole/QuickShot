using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSTest : MonoBehaviour {



	IEnumerator Start ()
    {
		if (!SystemInfo.supportsLocationService)
        {
            Debug.Log("No GPS.");
            yield break;
        }
        else if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Enable GPS Please");
            yield break;
        }

        while(SystemInfo.supportsLocationService && Input.location.isEnabledByUser)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            switch (Input.location.status)
            {
                case LocationServiceStatus.Running:
                    float lat = Input.location.lastData.latitude;
                    float lon = Input.location.lastData.longitude;
                    Debug.Log("LAT: " + lat + ", LON: " + lon);
                    break;
                case LocationServiceStatus.Stopped:
                    Input.location.Start();
                    break;
                default:
                    break;
            }
        }

	}
	
	void Update ()
    {
		
	}
}
