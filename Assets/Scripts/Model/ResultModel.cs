using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultModel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI perfectText;
    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] TextMeshProUGUI badText;
    [SerializeField] TextMeshProUGUI missText;

    private void OnEnable()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        perfectText.text = GameManager.Instance.Perfect.ToString();
        greatText.text = GameManager.Instance.Great.ToString();
        badText.text = GameManager.Instance.Bad.ToString();
        missText.text = GameManager.Instance.Miss.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene("MusicScene");
    }
}
