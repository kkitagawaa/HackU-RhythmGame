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

    private NotesManagerModel aNotesManager;

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

    private bool aIsGameStart = false;
    public bool IsGameStart
    {
        get
        {
            return this.aIsGameStart;
        }
        set
        {
            this.aIsGameStart = value;
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

    private int combo;
    public float Combo
    {
        get
        {
            return this.combo;
        }
        set
        {
            this.combo = (int)value;
        }
    }
    private int score;
    public float Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = (int)value;
        }
    }

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

    public void Start()
    {
        this.aNotesManager = FindObjectsByType<NotesManagerModel>(FindObjectsSortMode.None)[0];
    }

    public void Update()
    {
        if (this.aIsGameStart)
        {
            if (this.aNotesManager.NoteList.Count <= 0)
            {
                this.aIsGameStart = false;
                // MusicManager.Instance.Stop();
            }
        }
    }

    public void StartPlay()
    {
        this.aIsGameStart = true;
        this.aStartTime = Time.time;
        MusicManager.Instance.Play("タイフーンパレード");
    }
}