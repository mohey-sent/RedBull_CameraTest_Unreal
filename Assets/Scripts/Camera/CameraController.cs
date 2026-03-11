using UnityEngine;
using UnityEngine.UI;

public class CameraController : Singletons<CameraController>
{
    [SerializeField] RawImage background;
    private WebCamTexture backCam;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0) return;

        backCam = new WebCamTexture(devices[0].name);
        background.texture = backCam;
        backCam.Play();

    }

    public void StopCamera()
    {
        backCam.Pause();
    }
    public void PlayCamera()
    {
        backCam.Play();
    }
}
