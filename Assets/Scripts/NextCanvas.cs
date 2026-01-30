using UnityEngine;

public class NextCanvas : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float timer = 180f; // 3 minutes default (in seconds)

    [Header("Canvas References")]
    [SerializeField] private GameObject nextCanvas;
    [SerializeField] private GameObject thisCanvas;

    private float currentTime = 100;
    private bool timerRunning = false;

    void Start()
    {
        // Play on start
        currentTime = timer;
        timerRunning = true;

        // Make sure this canvas is enabled and next canvas is disabled at start
        if (thisCanvas != null)
        {
            thisCanvas.SetActive(true);
        }

        if (nextCanvas != null)
        {
            nextCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;

            // When timer finishes
            if (currentTime <= 0)
            {
                SwitchCanvas();
                timerRunning = false;
            }
        }
    }

    private void SwitchCanvas()
    {
        // Enable other canvas
        if (nextCanvas != null)
        {
            nextCanvas.SetActive(true);
        }

        // Disable this canvas
        if (thisCanvas != null)
        {
            thisCanvas.SetActive(false);
        }
    }
}