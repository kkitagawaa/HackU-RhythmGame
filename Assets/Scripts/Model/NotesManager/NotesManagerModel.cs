using System;
using System.Collections.Generic;
using UnityEngine;

///	<summary>
/// ノーツの情報を読み込み、管理するModel
/// </summary>
public class NotesManagerModel : MonoBehaviour
{

    private List<NoteData> aNoteList = new List<NoteData>();
    public List<NoteData> NoteList
    {
        get
        {
            return aNoteList;
        }
    }
    private List<GameObject> aNoteObjects = new List<GameObject>();

    [SerializeField] private GameObject aBaseNoteObject;

    public void OnEnable()
    {
        Load("タイフーンパレード");
    }

    /// <summary>
    /// ノーツのファイルを読み込み、必要な設定などの処理を行うメソッド。
    /// </summary>
    /// <param name="notesInfomationFileName">NoteEditorにて生成したノーツ情報ファイル名。拡張子.jsonは不要</param>
    private void Load(string notesInfomationFileName)
    {
        // notesInfomationFileNameに対応する譜面データをResourcesから取得し、parseする
        string aNotesInfomationFileNamePath = Resources.Load<TextAsset>(notesInfomationFileName).ToString();
        NotesInfomation aNotesInfomation = JsonUtility.FromJson<NotesInfomation>(aNotesInfomationFileNamePath);

        this.SetMaxScore(aNotesInfomation.Notes.Count);
        this.NotesLineUp(aNotesInfomation);
    }

    /// <summary>
    /// ノーツの情報を参考に、必要な数のノーツを生成し、並べ上げるメソッド
    /// </summary>
    /// <param name="aNotesInfomation">ノーツなどの情報</param>
    private void NotesLineUp(NotesInfomation aNotesInfomation)
    {
        float aNotesSpeed = GameManager.Instance.NoteSpeed;
        aNotesInfomation.Notes.ForEach((aNote) =>
        {
            float anInterval = 60 / (aNotesInfomation.BPM * (float)aNote.LPB);
            float aBeatSec = anInterval * aNote.LPB;
            float aTime = (aBeatSec * aNote.num / aNote.LPB) + aNotesInfomation.offset * 0.01f;

            aNote.ActionRequiredTime = aTime;
            this.aNoteList.Add(aNote);

            float aDistance = aTime * aNotesSpeed;
            this.aNoteObjects.Add(Instantiate(this.aBaseNoteObject, new Vector3(aNote.block - 1.5f, 0.55f, aDistance), Quaternion.identity));
        });
    }

    ///<summary>
    /// ノーツの数から最大スコアを算出し、設定するメソッド
    ///</summary>
    /// <param name="noteNum">ノーツの数</param>
    private void SetMaxScore(int noteNum)
    {
        GameManager.Instance.MaxScore = noteNum * 5;
    }
}