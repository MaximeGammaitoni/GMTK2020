using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager 
{

    public Text scorePanel;
    public Text comboPanel;
    public string MyString = "COOL";
    public float scoreValue = 0.00f;

    public float totalScore = 0f;

    public bool isStreaking;

    public int comboCounter = 0;

    private int scoreMultiplier = 1;



    // Start is called before the first frame update
    public ScoreManager()
    {
        isStreaking = false;
        scorePanel = GameObject.Find("Score").GetComponent<Text>();
        comboPanel = GameObject.Find("Combo").GetComponent<Text>();
    }

    public void SaveHighScore()
    {
        totalScore = scoreValue;
    }

    public void IncrementScore()
    {
        scoreValue += 0.10f;
        scorePanel.text = scoreValue.ToString();
    }

    public void MultiplieScore()
    {
        if(isStreaking && scoreMultiplier == 1)
        {
            scoreMultiplier++;
            scoreValue += (float)scoreMultiplier;
        }
        else if(isStreaking && scoreMultiplier == 2)
        {
            scoreMultiplier++;
            scoreValue += (float)scoreMultiplier;
        }
        else if(isStreaking && scoreMultiplier == 3)
        {
            scoreValue += (float)scoreMultiplier;
        }
        scorePanel.text = scoreValue.ToString();
    }

    public void ComboCounter()
    {
        if (isStreaking)
        {
            if(comboCounter < 3)
                comboCounter++;
        }
        else
        {
            comboCounter = 0;
        }
        comboPanel.text = "x "+comboCounter.ToString();
    }
}
