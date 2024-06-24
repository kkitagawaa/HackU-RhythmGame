using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class ScoreModel: MonoBehaviour
{

    private static ScoreModel instance = null;
    public static ScoreModel Instance
    {
        get
        {
            return ScoreModel.instance;
        }
    }
    private float aMaxScore = 0;
    public float MaxScore
    {
        get
        {
            return this.aMaxScore;
        }
        set
        {
            this.aMaxScore = value;
        }
    }

    private float aRatioScore = 0;
    public float RatioScore
    {
        get
        {
            return this.aRatioScore;
        }
    }

    private int combo = 0;
    public float Combo
    {
        get
        {
            return this.combo;
        }
    }
    private int score = 0;
    public float Score
    {
        get
        {
            return this.score;
        }
    }

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;

    private Dictionary<string, int> aJudgeCounts = new Dictionary<string, int>()
    {
        { "Perfect", 0 },
        { "Great", 0 },
        { "Bad", 0 },
        { "Miss", 0 }
    };
    public Dictionary<string, int> JudgeCounts
    {
        get
        {
            return this.aJudgeCounts;
        }
    }

    private Dictionary<string, int> aRatioScoreTable = new Dictionary<string, int>()
    {
        { "Perfect", 5 },
        { "Great", 3 },
        { "Bad", 1 },
        { "Miss", 0 }
    };

    public void Awake()
    {
        if (ScoreModel.instance == null)
        {
            ScoreModel.instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public void Reset()
    {
        this.aMaxScore = 0;
        this.aRatioScore = 0;
        this.combo = 0;
        this.score = 0;
        this.aJudgeCounts = new Dictionary<string, int>()
        {
            { "Perfect", 0 },
            { "Great", 0 },
            { "Bad", 0 },
            { "Miss", 0 }
        };
    }

    public void AddScore(string type)
    {
        this.aJudgeCounts[type]++;
        this.aRatioScore += this.aRatioScoreTable[type];

        if (type == "Miss")
        {
            this.combo = 0;
        }
        else
        {
            this.combo++;
        }

        this.score = (int) Math.Round(1000000 * Math.Floor(this.RatioScore / this.aMaxScore * 1000000) / 1000000);

        this.ApplyScoreText();
    }

    private void ApplyScoreText()
    {
        comboText.text = this.Combo.ToString();
        scoreText.text = this.Score.ToString();
    }
}
