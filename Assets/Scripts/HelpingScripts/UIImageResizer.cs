using UnityEngine;
using UnityEngine.UI;

public class UIImageResizer : MonoBehaviour
{
    [SerializeField] private RawImage targetImage;         
    [SerializeField] private Vector2[] sizes;           
    [SerializeField] private int currentIndex = 0;      

    void Start()
    {
        ApplySize();
    }

    public void ApplySize()
    {
        if (targetImage == null || sizes == null || sizes.Length == 0)
        {
            Debug.LogWarning("Missing reference or empty size list!");
            return;
        }

        // Clamp index to avoid out-of-range errors
        currentIndex = Mathf.Clamp(currentIndex, 0, sizes.Length - 1);

        // Apply width and height
        RectTransform rect = targetImage.rectTransform;
        rect.sizeDelta = sizes[currentIndex];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetIndex(0);
        }
    }

    // Optional: Call this from another script or UI button
    public void SetIndex(int index)
    {
        currentIndex = index;
        ApplySize();
    }
}
