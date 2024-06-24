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

    private UDPActionServer anUdpActionServer;
    private ExecutableRunner anExecutableRunner;

    // private HackURythmController aHackURythmController;

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

    private float aNoteSpeed = 4;
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
        if (!AudioIdentifierInstaller.IsInstall())
        {
            new AudioIdentifierInstaller().Install();
        }
        this.aNotesManager = FindObjectsByType<NotesManagerModel>(FindObjectsSortMode.None)[0];
        this.aEndTime = this.aNotesManager.NoteList[this.aNotesManager.NoteList.Count - 1].ActionRequiredTime;


        // welknown port 以外の範囲で、ランダムなポートを指定
        int aServerPort = Random.Range(50000, 55000);
        int aReceiverPort = Random.Range(55001, 60000);

        this.anUdpActionServer = UDPActionServer.Start(aServerPort, aReceiverPort, HackURythmController.Instance.JudgeCheck);

        this.anExecutableRunner = ExecutableRunner.Run(AudioIdentifierInstaller.ExecutablePath(),
                                              new string[] { aServerPort.ToString(), aReceiverPort.ToString() });
    }

    public void OnDestroy()
    {
        this.anExecutableRunner.Stop();
        this.anUdpActionServer.Stop();
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