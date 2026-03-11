using UnityEngine;
using System.Collections.Generic;
namespace MoheyBasicPack.JSON
{
    [CreateAssetMenu(fileName = "JSONData", menuName = "JSONData/PucksData")]
    public class PucksData : ScriptableObject
    {
        public string NameOfEvent;
        public bool IsRealTime;
        public List<int> pucksIds;
    }
}