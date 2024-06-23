using System;
using System.Collections.Generic;
using nkjzm.Tests;
using UnityEngine;
using TMPro;

///	<summary>
/// ノーツへのアクションに対して、音ゲーとしてのジャッジを行うModel。
/// </summary>
public class JudgeModel : MonoBehaviour
{
    private static readonly int SAME_EXECUTE_COUNT = 4;
    private Dictionary<string, GameObject> aJudgePopupObjects;

    private NotesManagerModel aNotesManager;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;

    public void Start()
    {
        this.aNotesManager = FindObjectsByType<NotesManagerModel>(FindObjectsSortMode.None)[0];

        this.aJudgePopupObjects = new Dictionary<string, GameObject>()
        {
            { "Perfect", Utils.LoadPrefab<GameObject>("Perfect") },
            { "Great", Utils.LoadPrefab<GameObject>("Great") },
            { "Bad", Utils.LoadPrefab<GameObject>("Bad") },
            { "Miss", Utils.LoadPrefab<GameObject>("Miss") }
        };
    }

    public void Judgement(string inputType)
    {
        if (!GameManager.Instance.IsGameStart) return;
        if (inputType != null)
        {
            this.aNotesManager.NoteList.GetRange(0, Math.Min(SAME_EXECUTE_COUNT, this.aNotesManager.NoteList.Count)).ForEach(aNote =>
            {
                if (aNote.TypeName == inputType)
                {
                    string passText = CheckPassAction(Math.Abs(Time.time - (aNote.ActionRequiredTime + GameManager.Instance.StartTime)));
                    if (passText != null)
                    {
                        this.presentationJudge(aNote, passText);
                        this.calculateScoreAndCombo(passText);
                        this.aNotesManager.NoteList.Remove(aNote);
                    }
                }
            });
        }
        else
        {

            NoteData nearestNote = this.aNotesManager.NoteList[0];
            if (Time.time > nearestNote.ActionRequiredTime + 0.2f + GameManager.Instance.StartTime) //本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
            {
                this.popUpJudge("Miss", nearestNote.LaneNumber);
                this.resetCombo();
                this.aNotesManager.NoteList.Remove(nearestNote);
            }
        }
    }
    private string CheckPassAction(float timeLag)
    {
        // audio.PlayOneShot(hitSound);
        if (timeLag <= 0.05) return "Perfect";
        if (timeLag <= 0.08) return "Great";
        if (timeLag <= 0.10) return "Bad";
        return null;
    }

    private void presentationJudge(NoteData aNote, string judgeKey)
    {
        this.popUpJudge(judgeKey, aNote.LaneNumber);
        this.particleBurst(aNote);
        aNote.NoteGameObject.SetActive(false);
    }

    private void particleBurst(NoteData aNote)
    {
        GameObject particleEffect = Utils.LoadPrefab<GameObject>("ParticleEffect");
        Instantiate(particleEffect, aNote.NoteGameObject.transform.position, Quaternion.identity);
    }

    private void popUpJudge(string judgeKey, int laneNumber)//判定を表示する
    {
        Instantiate(aJudgePopupObjects[judgeKey], new Vector3(laneNumber - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }

    private void calculateScoreAndCombo(string judgeKey)
    {
        switch (judgeKey)
        {
            case "Perfect":
                GameManager.Instance.RatioScore += 5;
                break;
            case "Great":
                GameManager.Instance.RatioScore += 3;
                break;
            case "Bad":
                GameManager.Instance.RatioScore += 1;
                break;
        }
        GameManager.Instance.Combo++;
        GameManager.Instance.Score = (int)Math.Round(1000000 * Math.Floor(GameManager.Instance.RatioScore / GameManager.Instance.MaxScore * 1000000) / 1000000);
        comboText.text = GameManager.Instance.Combo.ToString();
        scoreText.text = GameManager.Instance.Score.ToString();
    }

    private void resetCombo()
    {
        GameManager.Instance.Combo = 0;
        comboText.text = GameManager.Instance.Combo.ToString();
    }
}