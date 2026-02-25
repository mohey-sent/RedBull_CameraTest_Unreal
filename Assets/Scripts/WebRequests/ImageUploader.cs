using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageUploader : Singletons<ImageUploader>
{
    [SerializeField] string hostUrl;

    public void UploadImage(Texture2D image)
    {
        StartCoroutine(UploadeImage_CO(image));
    }
    IEnumerator UploadeImage_CO(Texture2D image)
    {
        byte[] photodata = image.EncodeToPNG();

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", photodata);
        
        UnityWebRequest www = UnityWebRequest.Post(hostUrl, form);
        {
            www.SetRequestHeader("accept", "application/json");

            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                yield return new WaitForEndOfFrame();
                print(www.result);
                QRCodeGenerator.Singleton.GenerateQRCode(www.downloadHandler.text);
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
