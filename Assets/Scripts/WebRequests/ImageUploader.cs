using MoheyBasicPack.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageUploader : Singletons<ImageUploader>
{
    [SerializeField] JSONReader jsonSettingsReader;
    [SerializeField] string hostUrl;
    [SerializeField] RawImage generatedQrCodeDisplayer;
    string api;
    SettingsData settingsData;
    private void Start()
    {
        Setup();
    }
    private void Setup()
    {
        settingsData = (SettingsData)jsonSettingsReader.GetData();
        hostUrl=hostUrl.Replace("IP",settingsData.hostIp);
        hostUrl=hostUrl.Replace("PORT",settingsData.hostPort);
        api = settingsData.ApiPath;
    }
    public void UploadImage(Texture2D image)
    {
        StartCoroutine(UploadeImage_CO(image));
    }
    IEnumerator UploadeImage_CO(Texture2D image)
    {
        byte[] photodata = image.EncodeToPNG();

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", photodata);
        
        UnityWebRequest www = UnityWebRequest.Post(hostUrl+api, form);
        {
            www.SetRequestHeader("accept", "application/json");

            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                yield return new WaitForEndOfFrame();
                print(www.result);
                string json = www.downloadHandler.text;

                UploadResponse response = JsonUtility.FromJson<UploadResponse>(json);

                string filePath = response.file.path;
                filePath=hostUrl+filePath;
                print(filePath);
                generatedQrCodeDisplayer.texture=QRCodeGenerator.Singleton.GenerateQRCode(filePath);
                //QRCodeGenerator.Singleton.GenerateQRCode(www.GetResponseHeader("imageUrl"));
            }
            else
            {
                print(www.error);
                print(www.downloadHandler.text);
                yield return null;
            }
        }
    }
}

[System.Serializable]
public class FileData
{
    public string filename;
    public string path;
    public int size;
}
[System.Serializable]
public class UploadResponse
{
    public bool success;
    public string message;
    public FileData file;
}
