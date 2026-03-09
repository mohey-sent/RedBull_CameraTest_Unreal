using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using RenderHeads.Media.AVProVideo;
public class FlowManager : MonoBehaviour
{
    [SerializeField] GameObject screenShot;
    [Space]
    [SerializeField] CameraController cameraController;
    [SerializeField] CanvasGroup startBtn;
    [SerializeField] CanvasGroup actionVideCG;
    [SerializeField] VideoPlayer actionVideoVP;
    [SerializeField] MediaPlayer idleVideoVP;
    [SerializeField] VideoPlayer captureVP;
    [SerializeField] RenderTexture mainVideoRT;
    [Space]
    [SerializeField] CanvasGroup capturePopUp;
    [SerializeField] float fadeDuration;
    [SerializeField] float popupDuration;
    [SerializeField] CanvasGroup QRcontainer;
    [Space]
    [Header("Predefined Sequence")]
    [SerializeField] float showPopupDelay;
    [SerializeField] float captureDelay;
    [SerializeField] float hidePopupDelay;
    [SerializeField] float qrShowDelay;
    [SerializeField] float restartDelay;
    RawImage actionVideoRawImage;
    WaitForSeconds showPopupDelaySeconds;
    WaitForSeconds hidePopupDelaySeconds;
    WaitForSeconds captureDelaySeconds;
    WaitForSeconds qrDelaySeconds;
    WaitForSeconds restartDelaySeconds;

    private void Awake()
    {
        SetupParameters();
    }
    private void SetupParameters()
    {
        actionVideoRawImage=actionVideoVP.transform.GetComponent<RawImage>();
        showPopupDelaySeconds=new WaitForSeconds(showPopupDelay);
        hidePopupDelaySeconds=new WaitForSeconds(hidePopupDelay);
        captureDelaySeconds=new WaitForSeconds(captureDelay);
        qrDelaySeconds = new WaitForSeconds(qrShowDelay);
        restartDelaySeconds=new WaitForSeconds(restartDelay);
    }
    //Un Comment to debug
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            screenShot.SetActive(!screenShot.activeSelf);
        if (Input.GetKeyDown(KeyCode.Escape))
            HideCapturePopup();
        if(Input.GetKeyDown(KeyCode.Return))
            CameraScreenshot.Singleton.Capture();
        if (Input.GetKeyDown(KeyCode.Tab))
            captureDelaySeconds = new WaitForSeconds(1000);
        if (Input.GetKeyDown(KeyCode.LeftControl))
            captureDelaySeconds = new WaitForSeconds(6);
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }
    Coroutine popSequenceCoroutine;
    public void StartActiviationClick()
    {
        cameraController.PlayCamera();
        startBtn.interactable = false;
        startBtn.DOFade(0,fadeDuration);
        actionVideoVP.Play();
        idleVideoVP.Stop();
        actionVideCG.DOFade(1, fadeDuration);
        popSequenceCoroutine= StartCoroutine(CO_PopUpssequence());
    }
    public void BackToIdleClick()
    {
        StopCoroutine(popSequenceCoroutine);
        ReturnToIdle();
    }
    IEnumerator CO_PopUpssequence()
    {
        yield return showPopupDelaySeconds;
        ShowCapturePopup();
        yield return captureDelaySeconds;
        CameraScreenshot.Singleton.Capture();
        yield return hidePopupDelaySeconds;
        HideCapturePopup();
        yield return qrDelaySeconds;
        ShowQrCode();
        yield return restartDelaySeconds;
        ReturnToIdle();
    }
    private void ShowCapturePopup()
    {
        capturePopUp.DOFade(1, fadeDuration);
        capturePopUp.transform.DOScale(1, popupDuration);
        captureVP.Play();
    }
    private void HideCapturePopup()
    {
        capturePopUp.DOFade(0, fadeDuration);
        capturePopUp.transform.DOScale(0, fadeDuration).SetDelay(fadeDuration).OnComplete(()=> 
        { 
            captureVP.Stop();
        });

        cameraController.StopCamera();
    }
    private void ShowQrCode()
    {
        QRcontainer.interactable = true;
        QRcontainer.DOFade(1, fadeDuration).OnComplete(()=> { QRcontainer.blocksRaycasts = true; });

    }
    private void ReturnToIdle()
    {
        actionVideCG.DOFade(0, fadeDuration).OnComplete(() =>
        {
            StartCoroutine(CO_RefreshMainVideoPlayer());
        });

        QRcontainer.interactable = false;
        QRcontainer.blocksRaycasts = false;
        QRcontainer.DOFade(0, fadeDuration);

        startBtn.DOFade(1, fadeDuration);
        idleVideoVP.Play();

        startBtn.interactable = true;
    }
    IEnumerator CO_RefreshMainVideoPlayer()
    {
        actionVideoVP.Stop();
        actionVideoVP.targetTexture = null;
        actionVideoRawImage.texture = null;
        yield return new WaitForEndOfFrame();
        mainVideoRT.Release();
        actionVideoVP.targetTexture = mainVideoRT;
        actionVideoRawImage.texture = actionVideoVP.targetTexture;
    }
}
