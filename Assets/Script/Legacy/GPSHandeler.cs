using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSHandeler : MonoBehaviour
{
    public static GPSHandeler Instance { set; get; }
    public double latitude;
    public double longitude;
    private double latPre;
    private double lonPre;
    public static int worldGPSDecimalPlaceOffset = 5;

    public double testLatt;
    public double testLon;



    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }
    public void Update()
    {
        UpdateGPS();
        //latitude = Input.location.lastData.latitude;
        //longitude = Input.location.lastData.longitude;
    }

    private IEnumerator UpdateGPS()
    {
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determin device location");
            
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        yield break;

    }
    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            longitude = testLon;
            latitude = testLatt;
            //longitude = -83.010259;
            //latitude = 39.999815;

            //longitude = -83.000493;
            //latitude = 40.004498;
            Debug.Log("User has not enabled GPS");
            yield break;
        }

        Input.location.Start(0.5f, 0.5f);
        StartCoroutine(UpdateGPS());
        

    }

    public  Vector3 ConvertGPSToGameWorldPosition(double lat, double lon)
    {

        Vector3 v = new Vector3();
        //v.x = (lat - worldGPSOriginOffset.x) * Mathf.Pow(10, worldGPSDecimalPlaceOffset);
        //Debug.Log("LAT:" + lat + ", origin:" + userGPSOrigin[0]+" = "+ ((lat - userGPSOrigin[0]) * Mathf.Pow(10, worldGPSDecimalPlaceOffset)));
        v.z = (float)((lat - latitude) * Mathf.Pow(10, worldGPSDecimalPlaceOffset));
        v.x = (float)((lon - longitude) * Mathf.Pow(10, worldGPSDecimalPlaceOffset));

        return v;
    }

    public  double[] ConvertWorldPositionToGPS(Vector3 pos)
    {
        // TODO
        double[] d = new double[2];
        d[0] = (double)((pos.z / Mathf.Pow(10, worldGPSDecimalPlaceOffset)) + latitude);
        d[1] = (double)((pos.x / Mathf.Pow(10, worldGPSDecimalPlaceOffset)) + longitude);
        return d;
    }

    public double[] returnCorrd()
    {
        double[] gps = { latitude, longitude };
        return gps;
    } 
}
