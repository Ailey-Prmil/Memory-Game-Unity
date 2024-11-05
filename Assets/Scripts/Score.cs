using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static Score instance;
    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Score();
            }

            return instance;
        }
    }

    public int Scores;
    private ScoreUnitManager scoreUnitManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreUnitManager = ScoreUnitManager.Instance;
        Scores = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void IncrementScore()
    {
        Scores += scoreUnitManager.ScoreUnit;
    }
}
