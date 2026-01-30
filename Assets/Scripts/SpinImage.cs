using UnityEngine;

public class SpinImage : MonoBehaviour
{
    [Header("Spin Settings")]
    [SerializeField] private float spinSpeed = 360f; // Degrees per second

    private RectTransform rectTransform;
    private bool isSpinning = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isSpinning && rectTransform != null)
        {
            // Rotate the image continuously
            rectTransform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
        }
    }

    // On call start spinning image
    public void StartSpinning()
    {
        isSpinning = true;
    }

    // Optional: Stop spinning
    public void StopSpinning()
    {
        isSpinning = false;
    }

    // Optional: Toggle spinning on/off
    public void ToggleSpin()
    {
        isSpinning = !isSpinning;
    }
}
