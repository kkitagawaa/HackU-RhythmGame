using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return GameManager.instance;
        }
    }

    private float aMaxScore;
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

    private float aRatioScore;
    public float RatioScore
    {
        get
        {
            return this.aRatioScore;
        }
        set
        {
            this.aRatioScore = value;
        }
    }

    private float aNoteSpeed = 8;
    public float NoteSpeed
    {
        get
        {
            return this.aNoteSpeed;
        }
    }

    private bool aStart = false;
    public bool Start
    {
        get
        {
            return this.aStart;
        }
        set
        {
            this.aStart = value;
        }
    }
    private float aStartTime;
    public float StartTime
    {
        get
        {
            return this.aStartTime;
        }
        set
        {
            this.aStartTime = value;
        }
    }

    // private int combo;
    // private int score;

    // private int perfect;
    // private int great;
    // private int bad;
    // private int miss;

    public void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void StartPlay()
    {
        this.aStart = true;
        this.aStartTime = Time.time;
        MusicManager.Instance.Play("menuettm");
    }
}