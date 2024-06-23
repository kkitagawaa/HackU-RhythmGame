using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float aEndTime;

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

    private int perfect;
    public float Perfect
    {
        get
        {
            return this.perfect;
        }
        set
        {
            this.perfect = (int)value;
        }
    }
    private int great;
    public float Great
    {
        get
        {
            return this.great;
        }
        set
        {
            this.great = (int)value;
        }
    }
    private int bad;
    public float Bad
    {
        get
        {
            return this.bad;
        }
        set
        {
            this.bad = (int)value;
        }
    }
    private int miss;
    public float Miss
    {
        get
        {
            return this.miss;
        }
        set
        {
            this.miss = (int)value;
        }
    }
    
    [SerializeField] GameObject finish;

    private bool hasFished = false;

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
        this.aEndTime = this.aNotesManager.NoteList[this.aNotesManager.NoteList.Count - 1].ActionRequiredTime;

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

            if (!hasFished && (Time.time > this.aStartTime + this.aEndTime))
            {
                Debug.Log("Finish");
                hasFished = true;
                this.Finish();
                
            }
        }
    }

    public void StartPlay()
    {
        this.hasFished = false;
        this.aIsGameStart = true;
        this.aStartTime = Time.time;
        // this.resetStats();
        MusicManager.Instance.Play("タイフーンパレード");
    }

    public void Finish()
    {
        // this.aIsGameStart = false;
        this.finish.SetActive(true);
        Invoke("ResultScene", 3.0f);
    }

    private void resetStats()
    {
        this.aMaxScore = 0;
        this.aRatioScore = 0;
        this.combo = 0;
        this.score = 0;
        this.perfect = 0;
        this.great = 0;
        this.bad = 0;
        this.miss = 0;
    }

    private void ResultScene()
    {
        SceneManager.LoadScene("Result");
    }
}