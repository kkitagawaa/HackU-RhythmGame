using System;
using System.Collections.Generic;
using nkjzm.Tests;
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

    private Dictionary<string, GameObject> aBaseNoteObjects;

    public void OnEnable()
    {
        this.aBaseNoteObjects = new Dictionary<string, GameObject>()
        {
            { "DESK", Utils.LoadPrefab<GameObject>("DeskNote") },
            { "CLAP", Utils.LoadPrefab<GameObject>("ClapNote") },
            { "PET", Utils.LoadPrefab<GameObject>("PETNote") },
            // { "EMPTY_BOX", Utils.LoadPrefab<GameObject>("Miss") }
        };
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

            float aDistance = aTime * aNotesSpeed;
            GameObject aNoteGameObject = Instantiate(this.aBaseNoteObjects[aNote.TypeName], new Vector3(aNote.block - 1.5f, 0.55f, aDistance), Quaternion.identity);

            aNote.ActionRequiredTime = aTime;
            aNote.NoteGameObject = aNoteGameObject;
            this.aNoteList.Add(aNote);
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