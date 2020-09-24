using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;

public class LocalDataPreperation : MonoBehaviour
{
    //This class is to prepare and parse all the local data.

    private const double distance = 0.003;
    private static List<Trees> myTrees = new List<Trees>();
    private static List<Trees> removedTrees = new List<Trees>();
    private static List<Trees> futureTrees = new List<Trees>();
    private static Dictionary<string, Dictionary<string, object>> GeneralStory;
    private static Dictionary<string, Dictionary<string, object>> SpecialStory;
    private static Dictionary<string, Dictionary<string, object>> TreeBenifit;
    private static JSONObject parsedCurrentJson;
    private static JSONObject parsedFutureTrees;

    public void Awake()
    {
        GeneralStory = CSVReader.read(Resources.Load("GeneralStoryMapping") as TextAsset);
        SpecialStory = CSVReader.read(Resources.Load("SpecialStoryMapping") as TextAsset);
        TreeBenifit = CSVReader.read(Resources.Load("BenefitsByTree2") as TextAsset);
        TextAsset text = Resources.Load("currentTree") as TextAsset;
        parsedCurrentJson = new JSONObject(text.ToString());
        parsedCurrentJson = parsedCurrentJson["features"];
        TextAsset futureTree = Resources.Load("FutureTrees") as TextAsset;
        parsedFutureTrees = new JSONObject(futureTree.ToString());
        parsedFutureTrees = parsedFutureTrees["features"];
    }


    public static void UpdateTreeList()
    {
        double currentLat = GPSHandeler.Instance.latitude;
        double currentLon = GPSHandeler.Instance.longitude;
        double variation3 = currentLat + distance;
        double variation4 = currentLat - distance;
        double variation1 = currentLon + distance;
        double variation2 = currentLon - distance;
        myTrees.Clear();
        myTrees = new List<Trees>();
        removedTrees.Clear();
        removedTrees = new List<Trees>();
        foreach (JSONObject item in parsedCurrentJson.list)
        {
            if ((double)item["geometry"]["coordinates"][0].n <= variation1 && (double)item["geometry"]["coordinates"][0].n >= variation2 && (double)item["geometry"]["coordinates"][1].n <= variation3 && (double)item["geometry"]["coordinates"][1].n >= variation4)
            {
                Trees tree = new Trees();
                tree.CoordinatesX = (double)item["geometry"]["coordinates"][0].n;
                Debug.Log(tree.CoordinatesX);
                tree.CoordinatesY = (double)item["geometry"]["coordinates"][1].n;
                tree.Genus = (string)item["properties"]["genus"].str;
                if (tree.Genus == "null")
                {
                    tree.Genus = "Unknown";
                }
                tree.Primarycom = (string)item["properties"]["primarycom"].str;
                if (tree.Primarycom == "null")
                {
                    tree.Primarycom = "Unknown";
                }
                tree.Plantsize = (float)item["properties"]["plantsize"].n;
                tree.Treeid = item["properties"]["treeid"].str;
                tree.Genuscode = (int)item["properties"]["genuscode"].n;
                tree.Specificep = (string)item["properties"]["specificep"].str;
                if (tree.Specificep == "null")
                {
                    tree.Specificep = "Unknown";
                }
                tree.Plantcondi = (string)item["properties"]["plantcondi"].str;
                tree.Dist = (double)item["properties"]["dist"].n;
                tree.Angle = (double)item["properties"]["angle"].n;

                string key = (tree.Genus != null && tree.Specificep != null) ? tree.Genus.ToLower() + " " + tree.Specificep.ToLower() : "no id";
                tree.SpecialStory = SpecialStory.ContainsKey(tree.Treeid.ToString()) ? (string)SpecialStory[tree.Treeid.ToString()]["Story"] : "Please contact us to provide more cultural story about this tree";
                tree.GeneralStory = GeneralStory.ContainsKey(key) ? (string)GeneralStory[key]["General Information"] + "\nTree id: " + tree.Treeid : "Please contact us to provide more cultural story about this tree" + "\nTree id: " + tree.Treeid;
                tree.GeneralStory = SpecialStory.ContainsKey(tree.Treeid.ToString()) ? (string)SpecialStory[tree.Treeid.ToString()]["Story"] : tree.GeneralStory;
                tree.EconomicBenifit = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? TreeBenifit[tree.Treeid.ToString()]["TotalAnnualBenefits_dolyr"].ToString() : "0";
                tree.HistoricalVal = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? TreeBenifit[tree.Treeid.ToString()]["StructuralValue_dol"].ToString() : "0";
                tree.CStorage = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["C_Storage_Dol"].ToString() : "0";
                tree.PollRemovel = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["Poll_Removal_dolyr"].ToString() : "0";
                tree.WaterMigration = TreeBenifit.ContainsKey(tree.Treeid.ToString()) ? (string)TreeBenifit[tree.Treeid.ToString()]["StructuralValue_dol"].ToString() : "0";
                //Debug.Log(tree.WaterMigration);

                if ((string)item["properties"]["plantcondi"].str == "Removed")
                {
                    tree.TreeType = 1;
                    tree.GeneralStory = "Congratulations on finding a ghost tree! This tree represents an " +
                        "actual removed tree on campus, which has been recorded by tree mappers over the past decade. " +
                        "Reasons for removal vary from disease, pests, storm damage, or, most commonly, campus construction." +
                        " Additionally, emerging forestry research sometimes identifies certain trees as invasive or harmful to the ecosystem, " +
                        "and campus planners will take steps to remove these species from the grounds. It is notable to acknowledge that as trees " +
                        "age they accumulate greater value, and are one of the only infrastructure elements that increase in value over time. " +
                        "For this reason, any removal of trees must be carefully weighed, as not all trees are equal. Currently, " +
                        "the average age of a campus tree is 17, but foresters cite the age of 25 to be when trees start providing maximum benefits to the ecosystem." + "\nTree id: " + tree.Treeid;
                    removedTrees.Add(tree);
                }
                else
                {
                    tree.TreeType = 0;
                    myTrees.Add(tree);

                }
                if(tree.Treeid == "9999999")
                {
                    tree.GeneralStory = (string)SpecialStory[tree.Treeid.ToString()]["Story"];
                }
                if (tree.Genus == "null")
                {
                    tree.GeneralStory = "Missing Data.Can you help identify this tree?”. Then a linkthat reads “Submit your data”. The link goes to thisform: https://arcg.is/1iXGLi0";
                }
            }
        }
        futureTrees.Clear();
        futureTrees = new List<Trees>();
        foreach (JSONObject item in parsedFutureTrees.list)
        {
            double x = (double)item["geometry"]["coordinates"][0].n;
            double y = (double)item["geometry"]["coordinates"][1].n;
            Vector2d meter = new Vector2d();
            meter[0] = x;
            meter[1] = y;
            Vector2d LatLon = Conversions.MetersToLatLon(meter);
            double newY = LatLon.x;
            double newX = LatLon.y;
            //Debug.Log(newX);
            if (newX <= variation1 && newX >= variation2 && newY <= variation3 && newY >= variation4)
            {
                Trees tree = new Trees();
                tree.CoordinatesX = newX;
                tree.CoordinatesY = newY;
                tree.Genus = "Future";
                tree.Primarycom = "Future";
                //tree.Specificep = (string)item["properties"]["specificep"].str;
                //string key = tree.Genus.ToLower() + " " + tree.Specificep.ToLower();
                tree.SpecialStory = "This is a future tree";
                tree.GeneralStory = "Congratulations on finding a future tree! " +
                    "As the name implies, this tree does not yet exist on our campus. Instead, it is " +
                    "a visualization of a significant OSU goal that had pledged to double the campus tree canopy " +
                    "by 2025. Currently, this goal has been edited to instead aim for a doubling of acreage that provides at " +
                    "least two ecosystem services. This adjustment provides greater leeway in terms of allowable land uses within the doubled acreage. " +
                    "It will also likely result in fewer trees planted as part of this initiative. These future trees were created in order to honor " +
                    "the original commitment, via a virtual space. ";
                tree.EconomicBenifit =  "0";
                tree.HistoricalVal = "0";
                tree.CStorage =  "0";
                tree.PollRemovel = "0";
                tree.WaterMigration =  "0";
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
