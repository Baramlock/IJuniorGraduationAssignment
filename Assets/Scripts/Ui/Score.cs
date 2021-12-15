using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        Player.ScoreChanged += ChangeScore;
    }

    private void OnDisable()
    {
        Player.ScoreChanged -= ChangeScore;
    }

    private void ChangeScore(int score)
    {
        _score.text = score.ToString();
    }

}
