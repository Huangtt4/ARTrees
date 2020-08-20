using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitTreeStory : MonoBehaviour
{
    public string Url = "https://docs.google.com/forms/d/e/1FAIpQLSdvLDyy6xt3H3xbG_bhQTikW9xu_sqad8aMT-uI6oIBki5PXA/viewform?entry.1257663404=";

    public void Open()
    {
        string Url2 = Url;
        Url2 += Fill_UI.returnTreeID().ToString();
        Application.OpenURL(Url2);
    }

}
