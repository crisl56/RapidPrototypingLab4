using UnityEngine;
using TMPro;

public class AddInventory : MonoBehaviour
{
    // Reference to TMP text
    public TextMeshProUGUI inventoryText;

    private int currentCount = 0;

    void Start()
    {
        UpdateText();
    }

    // Add to inventory
    public void AddItem()
    {
        currentCount++;
        UpdateText();
    }

    // Remove from inventory
    public void RemoveItem()
    {
        currentCount--;
        // Optional: prevent going below zero
        if (currentCount < 0)
        {
            currentCount = 0;
        }
        UpdateText();
    }

    // Update the text display
    private void UpdateText()
    {
        if (inventoryText != null)
        {
            inventoryText.text = "x" + currentCount.ToString();
        }
    }
}