using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Runtime.CompilerServices;

public class TreeParser : MonoBehaviour
{
    private static List<Trees> myTrees;
    private static List<Trees> removedTrees;
    private static List<Trees> futureTrees;
    private const double distance = 0.0007;
    //private static string jsonFile;
    private static Dictionary<string, Dictionary<string, object>> GeneralStory;
    private static Dictionary<string, Dictionary<string, object>> SpecialStory;
    private static Dictionary<string, Dictionary<string, object>> TreeBenifit;
    private double lastLon;
    private double lastLa;
    private float timer = 0;
    private static JSONObject parsedCurrentJson;
    private static JSONObject parsedFutureTrees;


    public void Awake()
    {
        timer = 0;
        GeneralStory = CSVReader.read(Resources.Load("GeneralStoryMapping") as TextAsset);

        SpecialStory = CSVReader.read(Resources.Load("SpecialStoryMapping") as TextAsset);

        TreeBenifit = CSVReader.read(Resources.Load("BenefitsByTree2") as TextAsset);
        //foreach(string key in TreeBenifit.Keys)
        //{
        //   Debug.Log(key);
        //}
        //string fileName = "Assets/Models/Tree/currentTree.json";
        TextAsset text = Resources.Load("currentTree") as TextAsset;
        parsedCurrentJson = new JSONObject(text.ToString());
        parsedCurrentJson = parsedCurrentJson["features"];
        TextAsset futureTree = Resources.Load("FutureTrees") as TextAsset;
        parsedFutureTrees = new JSONObject(futureTree.ToString());
        parsedFutureTrees = parsedFutureTrees["features"];

        lastLa = GPSHandeler.Instance.latitude;
        lastLon = GPSHandeler.Instance.longitude;
        UpdateTreeList();
        DontDestroyOnLoad(gameObject);


    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 6 && timer != -1)
        {
            UpdateTreeList();
          timer = -1;
        }
        
        if (GPSHandeler.Instance.latitude - lastLa >= 0.000499 || GPSHandeler.Instance.longitude - lastLon >= 0.000499)
        {
            UpdateTreeList();
            lastLa = GPSHandeler.Instance.latitude;
            lastLon = GPSHandeler.Instance.longitude;
        }

    }

    public static void UpdateTreeList()
    {
        Vector3 currentPos = new Vector3(0, 0);
        double[] geoPos = GPSHandeler.Instance.returnCorrd();
        double currentLat = GPSHandeler.Instance.latitude;
        double currentLon = GPSHandeler.Instance.longitude;
        double variation3 = currentLat + distance;
        double variation4 = currentLat - distance;
        double variation1 = currentLon + distance;
        double variation2 = currentLon - distance;
        //IEnumerable<JToken> trees = o.SelectTokens("$.features[?(@.geometry.coordinates[0] <= "+ variation1 + " && @.geometry.coordinates[0] >= " + variation2 + " && @.geometry.coordinates[1] <= " + variation3 + " && @.geometry.coordinates[1] >= " + variation4 + " )]");
        


        myTrees = new List<Trees>();
        removedTrees = new List<Trees>();
        foreach (JSONObject item in parsedCurrentJson.list )
        {
            if ((double)item["geometry"]["coordinates"][0].n <= variation1 && (double)item["geometry"]["coordinates"][0].n >= variation2 && (double)item["geometry"]["coordinates"][1].n <= variation3 && (double)item["geometry"]["coordinates"][1].n >= variation4)
            {
                Trees tree = new Trees();
                tree.CoordinatesX = (double)item["geometry"]["coordinates"][0].n;
                tree.CoordinatesY = (double)item["geometry"]["coordinates"][1].n;
                tree.Genus = (string)item["properties"]["genus"].str;
                tree.Primarycom = (string)item["properties"]["primarycom"].str;
                tree.Plantsize = (float)item["properties"]["plantsize"].n;
                tree.Treeid = item["properties"]["treeid"].str;
                tree.Genuscode = (int)item["properties"]["genuscode"].n;
                tree.Specificep = (string)item["properties"]["specificep"].str;
                tree.Plantcondi = (string)item["properties"]["plantcondi"].str;
                tree.Dist = (double)item["properties"]["dist"].n;
                tree.Angle = (double)item["properties"]["angle"].n;

                string key = tree.Genus.ToLower() + " " + tree.Specificep.ToLower();
                tree.SpecialStory = SpecialStory.ContainsKey(tree.Treeid.ToString()) ? (string)SpecialStory[tree.Treeid.ToString()]["Story"] : "Please contact us to provide more cultural story about this tree";
                tree.GeneralStory =GeneralStory.ContainsKey(key) ? (string)GeneralStory[key]["General Information"] : "Please contact us to provide more cultural story about this tree";
                tree.EconomicBenifit = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? TreeBenifit[tree.Treeid.ToString()]["TotalAnnualBenefits_dolyr"].ToString() : "data not kown";
                tree.HistoricalVal = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? TreeBenifit[tree.Treeid.ToString()]["StructuralValue_dol"].ToString() : "data not kown";
                tree.CStorage = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["C_Storage_lb"].ToString() : "data not kown";
                tree.PollRemovel = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["Poll_Removal_ozyr"].ToString() : "data not kown";
                tree.WaterMigration = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["AvoidedRunoff_Dol"].ToString() : "data not kown";
                //Debug.Log(tree.WaterMigration);

                if ((string)item["properties"]["plantcondi"].str == "Removed")
                {
                    tree.TreeType = 1;
                    removedTrees.Add(tree);
                }
                else
                {
                    tree.TreeType = 0;
                    myTrees.Add(tree);
                    
                }
            }
        }
        futureTrees = new List<Trees>();
        foreach (JSONObject item in parsedFutureTrees.list)
        {
            if ((double)item["geometry"]["coordinates"][0].n <= variation1 && (double)item["geometry"]["coordinates"][0].n >= variation2 && (double)item["geometry"]["coordinates"][1].n <= variation3 && (double)item["geometry"]["coordinates"][1].n >= variation4)
            {
                Trees tree = new Trees();
                tree.CoordinatesX = (double)item["geometry"]["coordinates"][0].n;
                tree.CoordinatesY = (double)item["geometry"]["coordinates"][1].n;
                tree.Genus = "Future";
                tree.Primarycom = "Future";
                string key = tree.Genus.ToLower() + " " + tree.Specificep.ToLower();
                tree.SpecialStory = SpecialStory.ContainsKey(tree.Treeid.ToString()) ? (string)SpecialStory[tree.Treeid.ToString()]["Story"] : "This is a future tree";
                tree.GeneralStory = GeneralStory.ContainsKey(key) ? (string)GeneralStory[key]["General Information"] : "This is a future tree";
                tree.EconomicBenifit = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["TotalAnnualBenefits_dolyr"] : "data not kown";
                tree.HistoricalVal = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["StructuralValue_dol"] : "data not kown";
                tree.CStorage = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["C_Storage_lb"] : "data not kown";
                tree.PollRemovel = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["Poll_Removal_ozyr"] : "data not kown";
                tree.WaterMigration = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["DBH"] : "data not kown";
                tree.TreeType = 2;
                futureTrees.Add(tree);
            }
        }

    }

    public static List<Trees> getMyTrees()
    {
        return myTrees;
    }

    public static List<Trees> getRemovedTrees()
    {
        return removedTrees;
    }

    public static List<Trees> getFutureTrees()
    {
        return futureTrees;
    }

}