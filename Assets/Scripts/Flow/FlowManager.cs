using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowManager : MonoBehaviour
{
    [SerializeField] RawImage qrRI;
    [SerializeField] string qrUrl;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraScreenshot.Singleton.Capture();
        }    
    }
}
