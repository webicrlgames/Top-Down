using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount = 1)
    {
        score += amount;

        OnScoreChanged?.Invoke(score);
    }

    public int GetScore()
    {
        return score;
    }
}