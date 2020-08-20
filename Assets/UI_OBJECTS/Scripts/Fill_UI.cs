using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill_UI : MonoBehaviour
{
    public static AbstractTree _abstractTree = new AbstractTree();
    Text[] newtext;
    Text facts;
    public static GameObject journalManager, bookCover2, mainPg, tabs, aboutUsPg, sharePg, microscopePg, myTreeCollectionPg, instructionPg, treeBenefitsExplain;
    [SerializeField] Image leafImage;
    [SerializeField] GameObject culturalData;
    [SerializeField] Texture map, cult;
    void Start()
    {
        newtext = transform.GetChild(2).Find("MainPg").GetChild(0).GetComponentsInChildren<Text>();
        facts = transform.GetChild(2).Find("MainPg").Find("ScrollRect").GetChild(0).GetChild(0).GetComponent<Text>();

        journalManager = transform.GetChild(2).gameObject;
        bookCover2 = transform.GetChild(2).Find("BookCover2").gameObject;
        mainPg = transform.GetChild(2).Find("MainPg").gameObject;
        tabs = transform.GetChild(2).Find("Tabs").gameObject;

        aboutUsPg = transform.GetChild(2).Find("AboutUsPg").gameObject;
        sharePg = transform.GetChild(2).Find("SharePg").gameObject;
        microscopePg = transform.GetChild(2).Find("MicroscopePg").gameObject;
        myTreeCollectionPg = transform.GetChild(2).Find("MyTreeCollectionPg").gameObject;
        instructionPg = transform.GetChild(2).Find("InstructionsPg").gameObject;
        treeBenefitsExplain = transform.GetChild(2).Find("TreeBenefitsExplain").gameObject;
    }

    public static void OpenUI()
    {
        journalManager.SetActive(true);
        bookCover2.SetActive(true);
        mainPg.SetActive(true);
        tabs.SetActive(true);

        aboutUsPg.SetActive(false);
        sharePg.SetActive(false);
        microscopePg.SetActive(false);
        myTreeCollectionPg.SetActive(false);
        instructionPg.SetActive(false);
        treeBenefitsExplain.SetActive(false);

        //backgroundFade.SetActive(true);
    }

    public static int returnTreeID()
    {
        return _abstractTree.ID;
    }

    private void Update()
    {
        /*
         * Instead of using a table to load every title from an external csv
         * the naming convention of all of the leaves follow the same scheme,
         * meaning they're the genus + _ + specificep (the csv's name for scientificName).
         * 
         * You'll get these values when you create each individual AbstractTree
         * from the csv file included in the resources folder: BenefitsByTree2.csv
         */
        if (_abstractTree.treeType == 0)
        {


            string findLeaf = _abstractTree.genus + "_" + _abstractTree.scientificName;
            findLeaf = "LeafImages/" + findLeaf.ToLower();
            //Debug.Log(findLeaf);

            if (Resources.Load<Sprite>(findLeaf) == null)
            {
                leafImage.sprite = Resources.Load<Sprite>("LeafImages/LeafNotFoundIcon");
            }
            else
            {
                leafImage.sprite = Resources.Load<Sprite>(findLeaf);
            }
        }
        else if(_abstractTree.treeType == 1)
        {
            leafImage.sprite = Resources.Load<Sprite>("LeafImages/removedleaf");

        }
        else if (_abstractTree.treeType == 2)
        {
            leafImage.sprite = Resources.Load<Sprite>("LeafImages/futureleaf");

        }

        newtext[0].text = _abstractTree.commonName;
        newtext[1].text = _abstractTree.genus + " " + _abstractTree.scientificName;
        newtext[2].text = _abstractTree.diameter.ToString();
        newtext[3].text = "$" + _abstractTree.stormWaterMigration.ToString();
        newtext[4].text = "$" + _abstractTree.populationRemoval.ToString();
        newtext[5].text = "$" + _abstractTree.carbonStorage.ToString();
        newtext[6].text = "$" + _abstractTree.annualEconomicBenefit.ToString();
        facts.text = _abstractTree.facts;

        //if(_abstracttree.culturaldata == null)
        //{
        //    culturaldata.getcomponent<rawimage>().texture = map;
        //    culturaldata.transform.getchild(0).gameobject.setactive(false);
        //    culturaldata.transform.getchild(1).gameobject.setactive(false);
        //    culturaldata.transform.getchild(2).gameobject.setactive(false);
        //}
        //else
        //{
        //    culturaldata.getcomponent<rawimage>().texture = cult;
        //    culturaldata.transform.getchild(0).gameobject.setactive(true);
        //    culturaldata.transform.getchild(1).gameobject.setactive(true);
        //    culturaldata.transform.getchild(2).gameobject.setactive(true);
        //    culturaldata.transform.getchild(2).getcomponent<text>().text = _abstracttree.culturaldata;
        //}
    }

}
