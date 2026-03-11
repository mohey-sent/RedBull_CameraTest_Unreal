using UnityEngine;
using System.Collections.Generic;
namespace MoheyBasicPack.JSON
{
    [CreateAssetMenu(fileName = "JSONData", menuName = "JSONData/Settings")]
    public class SettingsData : ScriptableObject
    {
        public string hostIp;
        public string hostPort;
        public string ApiPath;
        public float pauseDelay;
        public float pauseDuration;
    }
}