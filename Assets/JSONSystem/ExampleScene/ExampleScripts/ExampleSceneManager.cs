using UnityEngine;
using TMPro;
using MoheyBasicPack.JSON;
public class ExampleSceneManager : MonoBehaviour
{
    [SerializeField] GameObject Instructions;

    [Space]
    [Space]
    [Header("Display")]
    [SerializeField] JSONReader displayJSON;
    [SerializeField] Transform displaysParent;
    [SerializeField] Transform staticDisplayTextsParent;
    [SerializeField] TextMeshProUGUI displayTextPrefab;
    [Space]
    [Space]
    [Header("Pucks")]
    [SerializeField] JSONReader pucksJSON;
    [SerializeField] TextMeshProUGUI nameOfEvent;
    [SerializeField] TextMeshProUGUI isRealTime;
    [SerializeField] Transform pucksIdsParent;
    [SerializeField] Transform staticPucksIdsParent;
    [SerializeField] TextMeshProUGUI pucksTextPrefab;

    private void Start()
    {
        Instructions.SetActive(false);
        GetDisplayData();
        GetPucksData();
    }
    public void GetDisplayData()
    {
        DisplayJSON displayJSONdata = (DisplayJSON)displayJSON.GetData();
        int displaysCount = displayJSONdata.displayTarget.Length;
        for (int i = 0; i < displaysCount; i++)
        {
            Instantiate(displayTextPrefab, staticDisplayTextsParent).text =
               $"Display {i} : ";

            Instantiate(displayTextPrefab, displaysParent).text =
               $"Camera {displayJSONdata.displayTarget[i].ToString()}";
        }
    }
    public void GetPucksData()
    {
        PucksData pucksData = (PucksData)pucksJSON.GetData();

        nameOfEvent.text = pucksData.NameOfEvent;
        isRealTime.text = pucksData.IsRealTime.ToString();

        for (int i = 0; i < pucksData.pucksIds.Count; i++)
        {
            Instantiate(pucksTextPrefab, staticPucksIdsParent).text =
                $"Puck Number {i} : ";

            Instantiate(pucksTextPrefab, pucksIdsParent).text =
               $"ID = {pucksData.pucksIds[i].ToString()}";
        }

    }
}
