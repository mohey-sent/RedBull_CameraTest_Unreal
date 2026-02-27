using DG.Tweening;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraScreenshot : Singletons<CameraScreenshot>
{
    [SerializeField] RawImage screenShotDisplayer;
    [SerializeField] CanvasGroup flash;
    [SerializeField] bool SavePhotos;
    [SerializeField] int captureX = 100;
    [SerializeField] int captureY = 100;
    [SerializeField] int captureWidth = 400;
    [SerializeField] int captureHeight = 300;

    public void Capture()
    {
        StartCoroutine(CaptureScreenSection());
        FlashScreen();
    }
    private void FlashScreen()
    {
        flash.DOFade(1, 0.1f).OnComplete(() =>
        {
            flash.DOFade(0, 0.5f);
        });
    }

    IEnumerator CaptureScreenSection()
    {
        yield return new WaitForEndOfFrame();
        
        Rect captureRect = new Rect(captureX, captureY, captureWidth, captureHeight);

        Texture2D screenShot = new Texture2D((int)captureRect.width, (int)captureRect.height, TextureFormat.RGB24, false);

        screenShot.ReadPixels(captureRect, 0, 0);
        screenShot.Apply(); // Apply the changes to the texture

        // Optional: Save the captured pixels to a PNG file
        if (SavePhotos)
            SavePhoto(screenShot);

        screenShotDisplayer.texture=screenShot;
        ImageUploader.Singleton.UploadImage(screenShot);
    }
    private void SavePhoto(Texture2D screenShot)
    {
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = string.Format("{0}/screen_{1}x{2}_{3}.png", Application.streamingAssetsPath, captureWidth, captureHeight, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        File.WriteAllBytes(filename, bytes);
        Debug.LogFormat("Screenshot saved to: {0}", filename);
    }
}