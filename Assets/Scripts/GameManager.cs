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

    
    [SerializeField] GameObject finish;

    private bool hasFished = false;

    public void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
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
            if (!hasFished && (Time.time > this.aStartTime + this.aEndTime))
            {
                Debug.Log("Finish");
                hasFished = true;
                this.aIsGameStart = false;
                this.Finish();
            }
        }
    }

    public void StartPlay()
    {
        this.hasFished = false;
        this.aIsGameStart = true;
        this.aStartTime = Time.time;
        MusicManager.Instance.Play("タイフーンパレード");
    }

    public void Finish()
    {
        // this.aIsGameStart = false;
        GameObject finishObject = GameObject.Find("Finish"); 
        this.finish.SetActive(true);
        Invoke("ResultScene", 2.0f);
    }

    private void Reset()
    {
        this.aNotesManager = FindObjectsByType<NotesManagerModel>(FindObjectsSortMode.None)[0];
        this.aEndTime = this.aNotesManager.NoteList[this.aNotesManager.NoteList.Count - 1].ActionRequiredTime;
        ScoreModel.Instance.Reset();
    }

    private void ResultScene()
    {
        SceneManager.LoadScene("Result");
    }
}