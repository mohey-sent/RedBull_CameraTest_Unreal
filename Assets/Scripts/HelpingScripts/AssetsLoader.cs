using System.IO;
using UnityEngine;
using UnityEngine.Video;

public static class AssetsLoader
{
    public static Sprite LoadImage(string path)
    {
        string filePath = path;
        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogError("Failed to load texture from image file");
                return null;
            }
        }
        else
        {
            Debug.LogError("Image file not found at path: " + filePath);
            return null;
        }
    }
}
