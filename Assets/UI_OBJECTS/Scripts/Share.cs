using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using VoxelBusters;
using VoxelBusters.NativePlugins;

public class Share : MonoBehaviour
{
	public void _ShareSocialMedia()
	{
		StartCoroutine(CaptureScreenShoot());
	}

	IEnumerator CaptureScreenShoot()
	{
		yield return new WaitForEndOfFrame();
		Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
		ShareSheet(texture);
		Object.Destroy(texture);
	}
	private void ShareSheet(Texture2D texture)
	{
		ShareSheet _shareSheet = new ShareSheet();

		_shareSheet.Text = "#ARTrees, #TreeHuggingSelfie";
		_shareSheet.AttachImage(texture);
		//_shareSheet.URL = "https://twitter.com.RoixoGames";

		NPBinding.Sharing.ShowView(_shareSheet, FinishSharing);
	}

	private void FinishSharing (eShareResult _result)
	{
		Debug.Log(_result);
	}
}
