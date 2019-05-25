using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

public class PlayerLocation : MonoBehaviour
{
    public Location Location { get; private set; }
    public TextMeshProUGUI TextMesh;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartcheckEnumerator());
        this.Location = new Location();
    }

    IEnumerator StartcheckEnumerator()
    {
        while(true)
        {
            int i = 0;
            while (!Input.location.isEnabledByUser)
            {
                Debug.Log(SystemInfo.deviceName);
                //textMesh.SetText(SystemInfo.deviceName);
                Permission.RequestUserPermission(Permission.FineLocation);
                yield return new WaitForSeconds(1);
                //textMesh.text = i + " Error";
                Debug.LogError("Failed to start gps! Is it enabled?");
                //TextMesh.text = "No GPS (" + i + ")";
                this.Location = null;
                yield return new WaitForSeconds(5);
                i++;
            }

            Input.location.Start(1, 0.1f);

            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return new WaitForSeconds(0.2f);
            }

            if (Input.location.status == LocationServiceStatus.Running)
            {
                this.Location = new Location(
                    Input.location.lastData.latitude,
                    Input.location.lastData.longitude,
                    Input.location.lastData.altitude,
                    Input.location.lastData.horizontalAccuracy,
                    Input.location.lastData.timestamp);
                /*textMesh.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude +
                                " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy +
                                " " + Input.location.lastData.timestamp;*/
            }
            Input.location.Stop();
        }
    }
}