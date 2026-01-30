using UnityEngine;
using UnityEngine.UI;

public class TurnBrown : MonoBehaviour
{
    // Context: UI canvas image
    [Header("Timer Settings")]
    [SerializeField] private float timer = 45f; // Default 45 seconds

    [Header("Color Settings")]
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color brownColor = new Color(0.55f, 0.27f, 0.07f); // Brown color

    private Image image;
    private float currentTime;
    private bool isLerping = false;

    void Start()
    {
        image = GetComponent<Image>();

        if (image != null)
        {
            // Store starting color
            startColor = image.color;
            currentTime = 0f;
            isLerping = true;
        }
    }

    void Update()
    {
        if (isLerping && image != null)
        {
            currentTime += Time.deltaTime;

            // Calculate lerp progress (0 to 1)
            float progress = Mathf.Clamp01(currentTime / timer);

            // Lerp to brown color
            image.color = Color.Lerp(startColor, brownColor, progress);

            // Stop lerping when timer finishes
            if (currentTime >= timer)
            {
                isLerping = false;
            }
        }
    }
}