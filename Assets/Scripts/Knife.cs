using UnityEngine;
using UnityEngine.EventSystems;

// NOTES: this is an image on a canvas
public class Knife : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rectTransform;
    private RectTransform parentCanvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = transform.parent.GetComponent<RectTransform>();
    }

    // When clicked on
    public void OnPointerClick(PointerEventData eventData)
    {
        MoveKnife();
        AddScore(300);
    }

    // Move knife to random location on parent canvas
    private void MoveKnife()
    {
        if (parentCanvas != null && rectTransform != null)
        {
            // Get canvas dimensions
            float canvasWidth = parentCanvas.rect.width;
            float canvasHeight = parentCanvas.rect.height;

            // Generate random position within canvas bounds
            float randomX = Random.Range(-canvasWidth / 2, canvasWidth / 2);
            float randomY = Random.Range(-canvasHeight / 2, canvasHeight / 2);

            // Set new position
            rectTransform.anchoredPosition = new Vector2(randomX, randomY);
        }
    }

    // Add score using singleton
    private void AddScore(int amount)
    {
        if (ScoreUI.Instance != null)
        {
            ScoreUI.Instance.AddScore(amount);
        }
    }
}