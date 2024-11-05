using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreUnitManager
{
    private static ScoreUnitManager instance;

    public int ScoreUnit
    {
        get;
        private set;
    }
    private int wrongMoves;

    ScoreUnitManager()
    {
        ScoreUnit = 20;
        wrongMoves = 0;
    }

    public static ScoreUnitManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreUnitManager();
            }

            return instance;
        }
    }

    public void IncrementWrongMoves()
    {
        wrongMoves++;
        if (wrongMoves is 10 or 15 or 20) ScoreUnit -= 5;
    }

}
