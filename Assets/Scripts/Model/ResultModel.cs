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
        ScoreModel aScoreModel = ScoreModel.Instance;
        scoreText.text = aScoreModel.Score.ToString();
        perfectText.text = aScoreModel.JudgeCounts["Perfect"].ToString();
        greatText.text = aScoreModel.JudgeCounts["Great"].ToString();
        badText.text = aScoreModel.JudgeCounts["Bad"].ToString();
        missText.text = aScoreModel.JudgeCounts["Miss"].ToString();
    }

    public void Retry()
    {
        MusicManager.Instance.Play("tap");
        SceneManager.LoadScene("MusicScene");
    }

    public void BackToSelect()
    {
        MusicManager.Instance.Play("tap");
        SceneManager.LoadScene("MusicSelect");
    }
}
