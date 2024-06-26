using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MusicSelectionManager : MonoBehaviour
{
    public Button[] musicButtons;
    public Button startButton;
    private string selectedMusic = null;

    private Button selectedButton;

    void Start()
    {
        foreach (Button btn in musicButtons)
        {
            btn.onClick.AddListener(() => SelectMusic(btn));
        }
        startButton.onClick.AddListener(StartMusicScene);
    }

    // 曲を選択するメソッド
    public void SelectMusic(Button btn)
    {
        if (selectedButton != null)
        {
            MusicManager.Instance.Stop();
            RectTransform rt = selectedButton.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(400, 70);
        }

        selectedButton = btn;
        selectedMusic = btn.GetComponentInChildren<TextMeshProUGUI>().text;

        // 選択されたボタンのサイズを変更
        RectTransform selectedRt = selectedButton.GetComponent<RectTransform>();
        selectedRt.sizeDelta = new Vector2(450, 100);
        DataManager.Instance.SelectedMusic = selectedMusic;
        MusicManager.Instance.Play(selectedMusic);
    }

    // シーンを開始するメソッド
    public void StartMusicScene()
    {
        if (!string.IsNullOrEmpty(selectedMusic))
        {
            MusicManager.Instance.Stop();
            SceneManager.LoadScene("MusicScene");
        }
        else
        {
            Debug.LogWarning("No music selected!");
        }
    }
}
