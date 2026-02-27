using TMPro;
using UnityEngine;

public class DebugRectResizer : MonoBehaviour
{
    [SerializeField] RectTransform target;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [Header("Resize Speed")]
    [SerializeField] float step = 5f;
    [SerializeField] float fastMultiplier = 5f;
    [SerializeField] float slowMultiplier = 0.2f;

    void Update()
    {
        if (target == null) return;

        float currentStep = step;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            currentStep *= fastMultiplier;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            currentStep *= slowMultiplier;

        Vector2 size = target.sizeDelta;

        // Width
        if (Input.GetKey(KeyCode.RightArrow))
            size.x += currentStep;

        if (Input.GetKey(KeyCode.LeftArrow))
            size.x -= currentStep;

        // Height
        if (Input.GetKey(KeyCode.UpArrow))
            size.y += currentStep;

        if (Input.GetKey(KeyCode.DownArrow))
            size.y -= currentStep;
        textMeshPro.text="width = " + size.x + "heigh = " + size.y;
        target.sizeDelta = size;
    }
}