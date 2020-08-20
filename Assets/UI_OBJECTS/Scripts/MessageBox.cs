using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private string UITextString = "Default Text";
    [SerializeField] CanvasGroup panel;
    [SerializeField] private Text UIText;
    float alpha;
    // Start is called before the first frame update
    void Start()
    {
        alpha = panel.alpha;
    }

    // Update is called once per frame
    void Update()
    {
        //First, set text
        //Second, popup at 100
        //Third, fade out to 0
    }

    public void UICreate(string displaytext)
    {
        UITextString = displaytext;
        UIText.text = UITextString;
        panel.alpha = 1;
        StartCoroutine(UIFade());
    }

    private IEnumerator UIFade()
    {
        yield return new WaitForSeconds(.025f);
        panel.alpha -= .025f;
        if(panel.alpha > 0)
        {
            StartCoroutine(UIFade());
        }
    }

}
