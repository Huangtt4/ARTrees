using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera myCamera;
    private bool takeScreenShot;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if(takeScreenShot)
        {
            takeScreenShot = false;
            RenderTexture rendTex = myCamera.targetTexture;

            Texture2D rendRes = new Texture2D(rendTex.width, rendTex.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, rendTex.width, rendTex.height);
            rendRes.ReadPixels(rect, 0, 0);

            byte[] byteArray = rendRes.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            Debug.Log("Screenshot Saved!");

            RenderTexture.ReleaseTemporary(rendTex);
            myCamera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenShot = true;
    }

    public void Screenshot()
    {
        TakeScreenshot(Screen.width, Screen.height);
    }
}
