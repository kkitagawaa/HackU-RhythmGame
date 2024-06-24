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

    public void Start()
    {
        if (!AudioIdentifierInstaller.IsInstall())
        {
            new AudioIdentifierInstaller().Install();
        }
        this.aNotesManager = FindObjectsByType<NotesManagerModel>(FindObjectsSortMode.None)[0];

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