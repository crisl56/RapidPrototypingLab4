using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

// Context: UI canvas image
public class FlipFood : MonoBehaviour, IPointerClickHandler
{
    [Header("Timer Settings")]
    [SerializeField] private float timer = 45f; // Default to 45s

    [Header("References")]
    [SerializeField] private TimerTextCountdown timerTextCountdown;
    [SerializeField] private RectTransform otherFoodImage;
    [SerializeField] private GameObject nextCanvas;
    [SerializeField] private GameObject currentCanvas;

    [Header("Animation Settings")]
    [SerializeField] private float moveDuration = 1f;

    [Header("Unity Event")]
    public UnityEvent onFoodFlipped;

    private RectTransform rectTransform;
    private float currentTime;
    private bool isCountingDown = false;
    private bool hasBeenClicked = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentTime = timer;
        isCountingDown = true;
    }

    void Update()
    {
        if (isCountingDown)
        {
            // Countdown from 45 to 0
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isCountingDown = false;
            }
        }
    }

    // When clicked on
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            OnFoodClicked();
        }
    }

    private void OnFoodClicked()
    {
        // Stop text timer
        if (timerTextCountdown != null)
        {
            timerTextCountdown.StopCountdown();
        }

        // Stop countdown in here
        isCountingDown = false;

        // Save how close the user is to 0
        float timeRemaining = currentTime;

        // Add score accordingly
        CalculateAndAddScore(timeRemaining);

        // Broadcast unity event
        onFoodFlipped?.Invoke();

        // Move to other food UI image smoothly
        if (otherFoodImage != null)
        {
            StartCoroutine(MoveToOtherFood());
        }
        else
        {
            // If no other food image, just wait and move to next canvas
            StartCoroutine(WaitAndMoveToNextCanvas());
        }
    }

    private void CalculateAndAddScore(float timeRemaining)
    {
        // Score calculation: closer to 0 = higher score
        // Perfect timing (0-2s remaining) = 1000 points
        // Good timing (2-5s remaining) = 500 points
        // Okay timing (5-10s remaining) = 300 points
        // Poor timing (10+ remaining) = 100 points

        int score = 0;

        if (timeRemaining <= 2f)
        {
            score = 1000;
        }
        else if (timeRemaining <= 5f)
        {
            score = 500;
        }
        else if (timeRemaining <= 10f)
        {
            score = 300;
        }
        else
        {
            score = 100;
        }

        // Add score using singleton
        if (ScoreUI.Instance != null)
        {
            ScoreUI.Instance.AddScore(score);
        }
    }

    private IEnumerator MoveToOtherFood()
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = otherFoodImage.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / moveDuration;

            // Smooth movement
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, progress);

            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;

        // Wait 3s then move to next canvas
        yield return new WaitForSeconds(3f);

        MoveToNextCanvas();
    }

    private IEnumerator WaitAndMoveToNextCanvas()
    {
        // Wait 3s then move to next canvas
        yield return new WaitForSeconds(3f);

        MoveToNextCanvas();
    }

    private void MoveToNextCanvas()
    {
        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
        }

        if (nextCanvas != null)
        {
            nextCanvas.SetActive(true);
        }
    }
}