using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info: MonoBehaviour
{
    public Canvas canvas;
    public Text Primarycom;
    public Text Genus;
    public Text Plantcondi;
    public Text Plantsize;
    public Text Treeid;
    public Text Genuscode;
    public Text Specificep;
    public Text Dist;
    public Text Angle;

    private void Start()
    {
        canvas.enabled = false;
    }

    public void UpdataInfo(Trees tree)
    {
        canvas.enabled = true;
        Primarycom.text = "Primarycom: " +  tree.Primarycom;
        Genus.text = "Genus: " + tree.Genus;
        Plantcondi.text = "Plantcondi: " + tree.Plantcondi;
        Plantsize.text = "Plantsize: " + tree.Plantsize;
        Treeid.text = "Treeid: " + tree.Treeid;
        Genuscode.text = "Genuscode: " + tree.Genuscode;
        Specificep.text = "Specificep: " + tree.Specificep;
        Dist.text = "Dist: " + tree.Dist;
        Angle.text = "Angle: " + tree.Angle;
    }

    public void OnDisable()
    {
        if (canvas != null)
            canvas.enabled = false;
    }

}
