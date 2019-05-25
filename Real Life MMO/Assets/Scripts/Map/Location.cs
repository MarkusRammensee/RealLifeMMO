using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Location
{
    public float latitude, longitude, altitude, horizontalAccuracy;
    public double timestamp;

    public Location() : this(0, 0, 0, 0, 0) { }

    public Location(float latitude, float longitude, float altitude,
        float horizontalAccuracy, double timestamp)
    {
        this.latitude = latitude;
        this.longitude = longitude;
        this.horizontalAccuracy = horizontalAccuracy;
        this.timestamp = timestamp;
    }

    public Vector3 ToVector()
    {
        return SphereToVector3(latitude, longitude, altitude);
    }

    internal Location Add(Vector3 direction)
    {
        Vector3 pos = SphereToVector3(latitude, longitude, altitude);

        pos += direction;
        Vector3 latLongAlt = SphereFromVector3(pos);

        return new Location(latLongAlt.x, latLongAlt.y, latLongAlt.z, horizontalAccuracy, DateTimeOffset.Now.ToUnixTimeMilliseconds());
    }

    public static Vector3 SphereFromVector3(Vector3 position)
    {
        float lat = (float)Math.Acos(position.y / position.magnitude); //theta
        float lon = (float)Math.Atan(position.x / position.z); //phi
        return new Vector3(lat * Mathf.Rad2Deg, lon * Mathf.Rad2Deg, position.magnitude);
    }

    private Vector3 SphereToVector3(float lat, float lon, float alt)
    {
        //an origin vector, representing lat,lon of 0,0. 

        Vector3 origin = new Vector3(0, 0, 1);
        //build a quaternion using euler angles for lat,lon
        Quaternion rotation = Quaternion.Euler(lat, lon, 0);
        //transform our reference vector by the rotation. Easy-peasy!
        Vector3 point = rotation * origin;

        return point * altitude;
    }
}