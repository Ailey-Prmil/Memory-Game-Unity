using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private ScoreAnimation scoreAnimation;
    private int Scores;
    private ScoreUnitManager scoreUnitManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreUnitManager = ScoreUnitManager.Instance;
        scoreAnimation = GetComponent<ScoreAnimation>();
        Scores = 0;
    }

    public void ChangeUnitScore()
    {
        scoreUnitManager.IncrementWrongMoves();
    }
    
    public void IncrementScore()
    {
        Scores += scoreUnitManager.ScoreUnit;
        scoreAnimation.SetScore(Scores);
        Debug.Log("Score: " + Scores);
    }

    public void ResetScore()
    {
        Scores = 0;
        scoreAnimation.SetScore(Scores);
    }


}
