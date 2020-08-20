using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TreeRequest : MonoBehaviour
{
    public static TreeRequest Instance { set; get; }
    private readonly static string baseString = "curio.osu.edu/trees";
    private readonly static string radius = "200";
    private readonly static string limit = "10000000"; // a big number so that all trees are returned within the radius
    public  string treeJson;

    public void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        getLocationAndRequestTrees();
    }

    public string getLocationAndRequestTrees()
    {
        //Getiing lat and lon from GPS handler
        string y = GPSHandeler.Instance.latitude.ToString();
        string x = GPSHandeler.Instance.longitude.ToString();

        string requestURL = baseString + "?x=" + x + "&y=" + y + "&dist=" + radius + "&limit=" + limit;
        Debug.Log(requestURL);

        StartCoroutine(getLocationAndRequestTrees(requestURL));

        return treeJson;
    }

     IEnumerator getLocationAndRequestTrees(string requestURL)
    {
        
        UnityWebRequest treeInfoRequest = UnityWebRequest.Get(requestURL);

        yield return treeInfoRequest.SendWebRequest();

        if (treeInfoRequest.isNetworkError || treeInfoRequest.isHttpError)
        {
            Debug.LogError(treeInfoRequest.error);
            yield break;
        }
        treeJson = treeInfoRequest.downloadHandler.text;

    }

}
