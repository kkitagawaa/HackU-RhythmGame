using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    private static GManager instance = null;
    public static GManager Instance
    {
        get
        {
            return instance;
        }
    }

    public float maxScore;
    public float ratioScore;

    public int songID;
    public float noteSpeed;

    public bool Start;
    public float StartTime;

    public int combo;
    public int score;

    public int perfect;
    public int great;
    public int bad;
    public int miss;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}