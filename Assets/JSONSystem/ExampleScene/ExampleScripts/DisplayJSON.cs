using UnityEngine;
namespace MoheyBasicPack.JSON
{
    [CreateAssetMenu(fileName = "JSONData", menuName = "JSONData/DiplaysData")]
    public class DisplayJSON : ScriptableObject
    {
        public int[] displayTarget;
    }
}