using System;
using System.Collections.Generic;
using nkjzm.Tests;
using UnityEngine;

///	<summary>
/// ノーツへのアクションに対して、音ゲーとしてのジャッジを行うModel。
/// </summary>
public class JudgeModel : MonoBehaviour
{
    private static readonly int SAME_EXECUTE_COUNT = 4;
    private Dictionary<string, GameObject> aJudgePopupObjects;

    private NotesManagerModel aNotesManager;

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

    public void Judgement(int laneNumber = -1)
    {
        if (!GameManager.Instance.Start) return;
        if (laneNumber > 0)
        {
            this.aNotesManager.NoteList.GetRange(0, SAME_EXECUTE_COUNT - 1).ForEach(aNote =>
            {
                if (aNote.LaneNumber == laneNumber)
                {
                    string passText = CheckPassAction(Math.Abs(Time.time - (aNote.ActionRequiredTime + GameManager.Instance.StartTime)));
                    if (passText != null)
                    {
                        this.popUpJudge(passText, aNote.LaneNumber);
                        this.aNotesManager.NoteList.Remove(aNote);
                    }
                }
            });
        }

        NoteData nearestNote = this.aNotesManager.NoteList[0];
        if (Time.time > nearestNote.ActionRequiredTime + 0.2f + GameManager.Instance.StartTime) //本来ノーツをたたくべき時間から0.2秒たっても入力がなかった場合
        {
            this.popUpJudge("Miss", nearestNote.LaneNumber);
            this.aNotesManager.NoteList.Remove(nearestNote);
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

    private void popUpJudge(string judgeKey, int laneNumber)//判定を表示する
    {
        Instantiate(aJudgePopupObjects[judgeKey], new Vector3(laneNumber - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}