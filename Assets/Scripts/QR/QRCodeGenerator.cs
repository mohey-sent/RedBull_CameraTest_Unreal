using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRCodeGenerator : Singletons<QRCodeGenerator>
{
    public Color backgroundColor = new Color(1, 1, 1, 0);
    public Color foregroundColor = Color.black;

    public  Texture2D GenerateQRCode(string text, int width = 256, int height = 256)
    {
        var qrWriter = new QRCodeWriter();
        var qrCode = qrWriter.encode(text, BarcodeFormat.QR_CODE, width, height);

        var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                texture.SetPixel(x, y, qrCode[x, y] ? foregroundColor : backgroundColor);
            }
        }
        texture.Apply();
        return texture;
    }
}