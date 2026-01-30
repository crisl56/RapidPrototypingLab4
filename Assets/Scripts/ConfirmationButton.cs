using UnityEngine;
using UnityEngine.UI;

public class ConfirmationButton : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField] private GameObject thisCanvas;
    [SerializeField] private GameObject[] nextCanvases;

    private Button button;
    private bool[] initialStates;

    void Start()
    {
        // Store initial states of next canvases
        if (nextCanvases != null && nextCanvases.Length > 0)
        {
            initialStates = new bool[nextCanvases.Length];
            for (int i = 0; i < nextCanvases.Length; i++)
            {
                if (nextCanvases[i] != null)
                {
                    initialStates[i] = nextCanvases[i].activeSelf;
                }
            }
        }

        // Get button component and add listener
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnConfirmation);
        }
    }

    // On call
    public void OnConfirmation()
    {

        for (int i = 0; i < nextCanvases.Length; i++)
        {
            if (nextCanvases[i] != null)
            {
                nextCanvases[i].SetActive(true);
            }
        }
        


        // Disable this canvas
        if (thisCanvas != null)
        {
            thisCanvas.SetActive(false);
        }
    }

    void OnDestroy()
    {
        // Clean up listener
        if (button != null)
        {
            button.onClick.RemoveListener(OnConfirmation);
        }
    }
}