using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.IO;

public class PhotoImage : MonoBehaviour, IPointerClickHandler
{
    [Header("Score Sprites")]
    [SerializeField] private Sprite goodScoreSprite;
    [SerializeField] private Sprite badScoreSprite;

    [Header("UI References")]
    [SerializeField] private Image scoreImage;
    [SerializeField] private Image flashImage;
    [SerializeField] private GameObject confirmationButton;

    [Header("Flash Settings")]
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private Color flashColor = Color.white;

    [Header("Screenshot Settings")]
    [SerializeField] private string screenshotFolder = "Screenshots";

    private bool hasBeenClicked = false;

    void OnEnable()
    {
        // Check score and change to appropriate sprite
        if (ScoreUI.Instance != null && ScoreUI.Instance.scoreData != null && scoreImage != null)
        {
            float currentScore = ScoreUI.Instance.scoreData.GetScore();

            if (currentScore < 2000)
            {
                // Show bad score sprite
                scoreImage.sprite = badScoreSprite;
            }
            else
            {
                // Show good score sprite if higher
                scoreImage.sprite = goodScoreSprite;
            }
        }

        // Make sure confirmation button is disabled initially
        if (confirmationButton != null)
        {
            confirmationButton.SetActive(false);
        }
    }

    // When clicked upon
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            StartCoroutine(TakePhoto());
        }
    }

    private IEnumerator TakePhoto()
    {
        // Create flash effect onto screen
        if (flashImage != null)
        {
            yield return StartCoroutine(FlashEffect());
        }

        // Screenshot current screen as image onto PC
        TakeScreenshot();

        // Enable confirmation button
        if (confirmationButton != null)
        {
            confirmationButton.SetActive(true);
        }
    }

    private IEnumerator FlashEffect()
    {
        flashImage.color = flashColor;
        flashImage.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);

            Color color = flashColor;
            color.a = alpha;
            flashImage.color = color;

            yield return null;
        }

        flashImage.gameObject.SetActive(false);
    }

    private void TakeScreenshot()
    {
        // Create screenshots folder if it doesn't exist
        string folderPath = Path.Combine(Application.persistentDataPath, screenshotFolder);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate filename with timestamp
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string filename = $"Screenshot_{timestamp}.png";
        string fullPath = Path.Combine(folderPath, filename);

        // Take screenshot
        ScreenCapture.CaptureScreenshot(fullPath);

        Debug.Log($"Screenshot saved to: {fullPath}");
    }
}