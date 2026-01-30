using UnityEngine;
using UnityEngine.UI;

public class FlipImage : MonoBehaviour
{
    // Context: UI Image
    [Header("Animation Settings")]
    [SerializeField] private float duration = 0.5f; // Duration of animation

    private RectTransform rectTransform;
    private bool isFlipping = false;
    private float currentTime = 0f;
    private Vector3 startScale;
    private Vector3 targetScale;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isFlipping && rectTransform != null)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / duration);

            // Lerp the scale to create flip effect
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);

            // Stop flipping when animation completes
            if (currentTime >= duration)
            {
                isFlipping = false;
                rectTransform.localScale = targetScale; // Ensure final scale is exact
            }
        }
    }

    // Public function to flip the image vertically (down to up)
    public void FlipVertically()
    {
        if (rectTransform != null && !isFlipping)
        {
            startScale = rectTransform.localScale;
            targetScale = new Vector3(startScale.x, -startScale.y, startScale.z);

            currentTime = 0f;
            isFlipping = true;
        }
    }
}