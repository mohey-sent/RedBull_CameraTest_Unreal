using UnityEngine;
using System.IO;
using UnityEngine.Video;
public class AssetsCacher : Singletons<AssetsCacher>
{
    [SerializeField] string imagesFolderPath = "/ImagePuck";
    [SerializeField] string imagesExtension = ".png";
    [Space]
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string videosFolderPath = "/Videos/";
    [SerializeField] string videoName = "long video";
    [SerializeField] string videoExtension = ".mp4";

    int imagesCount;
    Sprite[] imagePuckSprites;
    string fullPath;
    public Sprite[] GetImagePuckSprites => imagePuckSprites;
    private void Start()
    {
        LoadImages();
        LoadVideo();
    }
    private void LoadImages()
    {
        fullPath = Application.streamingAssetsPath + imagesFolderPath;
        if (Directory.Exists(fullPath))
        {
            string[] files = Directory.GetFiles(fullPath);
            imagesCount = files.Length;
#if UNITY_EDITOR
            imagesCount = files.Length / 2;
#endif
        }
        imagePuckSprites = new Sprite[imagesCount];
        for (int i = 0; i < imagesCount; i++)
        {
            imagePuckSprites[i] = AssetsLoader.LoadImage(fullPath + "/" + i + imagesExtension);
        }
    }
    public void LoadVideo()
    {
        videoPlayer.url = Application.streamingAssetsPath + videosFolderPath + videoName + videoExtension;
    }
    public VideoPlayer GetVideoPlayer=>videoPlayer;
}
