using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get
        {
            return MusicManager.instance;
        }
    }
    private AudioSource anAudioSource;
    private AudioClip anAudioClip;
    private bool aMusicPlayed = false;

    public bool MusicPlayed
    {
        get
        {
            return this.aMusicPlayed;
        }
    }

    public void Awake()
    {
        if (MusicManager.instance == null)
        {
            MusicManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }


    public void Play(string songName)
    {
        this.anAudioSource = this.GetComponent<AudioSource>();
        this.anAudioClip = Resources.Load<AudioClip>("Musics/" + songName);
        this.anAudioSource.volume = 0.3f;
        this.anAudioSource.PlayOneShot(this.anAudioClip);
        this.aMusicPlayed = true;
    }
    
    public void Stop()
    {
        this.anAudioSource.Stop();
        this.aMusicPlayed = false;
    }
}
