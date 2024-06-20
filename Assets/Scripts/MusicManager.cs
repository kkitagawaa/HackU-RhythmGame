using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audio;
    AudioClip Music;
    string songName;
    bool played;
    void Start()
    {
        GManager.Instance.Start = false;
        songName = "魔王魂  ファンタジー15";
        audio = GetComponent<AudioSource>();
        Music = Resources.Load<AudioClip>("Musics/" + songName);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!played)
        {
            GManager.Instance.Start = true;
            GManager.Instance.StartTime = Time.time;
            played = true;
            audio.PlayOneShot(Music);
        }
    }
}