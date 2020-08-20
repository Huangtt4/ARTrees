using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Examples;

public class Trees : MonoBehaviour
{
    //public Info info;
    public void setAttribute(Trees tree){ 
        this.CoordinatesX = tree.CoordinatesX;
        this.CoordinatesY = tree.CoordinatesY;
        this.Genus = tree.Genus;
        this.Primarycom = tree.Primarycom;
        this.Plantcondi = tree.Plantcondi;
        this.Plantsize = tree.Plantsize;
        this.Treeid = tree.Treeid;
        this.Genuscode = tree.Genuscode;
        this.Specificep = tree.Specificep;
        this.Dist = tree.Dist;
        this.Angle = tree.Angle;
        this.WaterMigration = tree.WaterMigration;
        this.PolRemoval = tree.PolRemoval;
        this.CStorage = tree.CStorage;
        this.AirQuality = tree.AirQuality;
        this.HistoricalVal = tree.HistoricalVal;
        this.EconomicBenifit = tree.EconomicBenifit;
        this.GeneralStory = tree.GeneralStory;
        this.SpecialStory = tree.SpecialStory;
        this.PollRemovel = tree.PollRemovel;
        this.TreeType = tree.TreeType;
    }
    [SerializeField] public double CoordinatesX { get; set; }
    [SerializeField] public double CoordinatesY { get; set; }
    [SerializeField] public string Genus { get; set; }
    [SerializeField] public string Primarycom { get; set; }
    [SerializeField] public float Plantsize { get; set; }
    [SerializeField] public string Treeid { get; set; }
    [SerializeField] public int Genuscode { get; set; }
    [SerializeField] public string Specificep { get; set; }
    [SerializeField] public string Plantcondi { get; set; }
    [SerializeField] public double Dist { get; set; }
    [SerializeField] public double Angle { get; set; }

    [SerializeField] public string WaterMigration { get; set; }
    [SerializeField] public string PolRemoval { get; set; }
    [SerializeField] public string CStorage { get; set; }
    [SerializeField] public string AirQuality { get; set; }
    [SerializeField] public string HistoricalVal { get; set; }

    [SerializeField] public string EconomicBenifit { get; set; }
    [SerializeField] public string GeneralStory { get; set; }
    [SerializeField] public string SpecialStory { get; set; }

    [SerializeField] public string PollRemovel { get; set; }

    [SerializeField] public int TreeType { get; set; }



    public void UpdateUI()
    {

        //info.UpdataInfo(this);
        //Debug.Log("CHeese:");
        //CameraBackground.OpenCamera();
        Fill_UI._abstractTree.diameter = this.Plantsize.ToString();
        Fill_UI._abstractTree.commonName = this.Primarycom;
        Fill_UI._abstractTree.scientificName = this.Specificep;
        Fill_UI._abstractTree.genus = this.Genus;

        Fill_UI._abstractTree.ID = (this.Treeid != null) ? int.Parse(this.Treeid) : -1;

        Fill_UI._abstractTree.stormWaterMigration = (this.WaterMigration != null) ?  this.WaterMigration : "nan";
        Fill_UI._abstractTree.populationRemoval = (this.PolRemoval != null) ? this.PolRemoval : "nan";
        //Fill_UI._abstractTree.CStorage = (this.CStorage != null) ? this.CStorage : "nan";
        //Fill_UI._abstractTree.airQualityValue = (this.AirQuality != null)?  this.AirQuality : "nan";
        Fill_UI._abstractTree.aestheticHistoricalValue = ( this.HistoricalVal != null) ? this.HistoricalVal : "nan";
        Fill_UI._abstractTree.annualEconomicBenefit = (this.EconomicBenifit != null) ? this.EconomicBenifit : "nan";
        GeneralStory = (GeneralStory != null) ? GeneralStory : "";
        SpecialStory = (SpecialStory != null) ? SpecialStory : "";
        //Debug.Log(this.GeneralStory);
        Fill_UI._abstractTree.facts = this.GeneralStory;
        Fill_UI._abstractTree.culturalData = this.SpecialStory;
        Fill_UI._abstractTree.aestheticHistoricalValue = this.HistoricalVal;
        Fill_UI._abstractTree.annualEconomicBenefit = this.EconomicBenifit;
        Fill_UI._abstractTree.carbonStorage = this.CStorage;
        Fill_UI._abstractTree.populationRemoval = this.PollRemovel;
        Fill_UI._abstractTree.stormWaterMigration = this.WaterMigration;
        Fill_UI._abstractTree.treeType = this.TreeType;
        Fill_UI.OpenUI();
        Vector2d location = new Vector2d();
        location.x = this.CoordinatesY;
        location.y = this.CoordinatesX;
        switch (this.TreeType)
        {
            case 0:
                AllIndicatorSpawn.currentLoc = location;
                break;
            case 1:
                AllIndicatorSpawn.removedLoc = location;
                break;
            case 2:
                AllIndicatorSpawn.futureLoc = location;
                break;
        }

        //Debug.Log(this.Treeid);
    }

    //private void OnMouseDown()
    //{
    //    //SenseSelection.hideMapMode();
    //    //SenseSelection.showFPMode();
    //    //GameObject.Find("Background").SetActive(true);
    //    //CameraBackground.OpenCamera();
    //    UpdateUI();
    //    //Debug.Log()
    //}
}
