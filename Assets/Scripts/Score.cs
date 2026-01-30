using UnityEngine;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score")]
public class Score : ScriptableObject
{
    [SerializeField] private float score = 0f;

    // Getter
    public float GetScore()
    {
        return score;
    }

    // Setter
    public void SetScore(float value)
    {
        score = value;
    }
}