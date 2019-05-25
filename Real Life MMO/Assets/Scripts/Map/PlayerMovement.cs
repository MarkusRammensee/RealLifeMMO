using UnityEngine;
using System.Collections;
using System;
using TMPro;
using Mapbox.Unity.Map;


[RequireComponent(typeof(PlayerLocation))]
public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Movement speed player. Does not follow any metrics!")]
    public float MovementSpeed;

    public TextMeshProUGUI textMesh;
    public AbstractMap abstractMap;

    private PlayerLocation playerLocation;
    private Location lastLocation;

    void Start()
    {
        playerLocation = GetComponent<PlayerLocation>();
    }
    private int i = 0;

    void Update()
    {
        if (playerLocation.Location != null)
            MoveTo(playerLocation.Location);
        else
        {
            //textMesh.text = "Error";
        }
    }

    private void MoveTo(Location location)
    {
        i++;
        if (lastLocation == null)
        {
            lastLocation = location;
        }
        //Vector3 direction = location.ToVector() - lastLocation.ToVector();
        //PlayerLocation= PlayerLocation + ;
        /*direction = direction.normalized * Mathf.Lerp(0, direction.magnitude, Time.deltaTime * MovementSpeed);
        if (direction.x == float.NaN)
            direction.x = 0;
        if (direction.y == float.NaN)
            direction.y = 0;
        if (direction.z == float.NaN)
            direction.z = 0;*/

        //location - lastLocation
       // lastLocation = lastLocation.Add(direction);
        abstractMap.UpdateMap(new Mapbox.Utils.Vector2d(lastLocation.latitude, lastLocation.longitude), abstractMap.Zoom);

        // Call animation thingy with direction
        textMesh.text = "Long: " + lastLocation.longitude + "\n" +
                        "Lat: " + lastLocation.latitude + "\n" +
                        "Alt: " + lastLocation.altitude + "\n" +
                        "I: "+i
                       /* +"\n\n (" + direction.x + ", " + direction.y + ", " + direction.z + ")"*/;
    }
}