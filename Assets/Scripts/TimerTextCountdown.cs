using UnityEngine;
using TMPro;

// Context: TMP Pro text
public class TimerTextCountdown : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float timer = 45f; // Default 45 seconds

    private TextMeshProUGUI timerText;
    private float currentTime;
    private bool isCountingDown = false;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        currentTime = timer;

        // On start set text to "Countdown: \n{default}"
        if (timerText != null)
        {
            UpdateTimerText();
            isCountingDown = true;
        }
    }

    void Update()
    {
        if (isCountingDown && timerText != null)
        {
            // Countdown slowly
            currentTime -= Time.deltaTime;

            // Clamp to 0 minimum
            if (currentTime < 0)
            {
                currentTime = 0;
                isCountingDown = false;
            }

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        // Showing only 2 decimal places (0.00 format)
        timerText.text = "Countdown: \n" + currentTime.ToString("F2");
    }

    // Public function that can be called to stop countdown
    public void StopCountdown()
    {
        isCountingDown = false;
    }

    // Optional: Restart countdown
    public void RestartCountdown()
    {
        currentTime = timer;
        isCountingDown = true;
        UpdateTimerText();
    }
}