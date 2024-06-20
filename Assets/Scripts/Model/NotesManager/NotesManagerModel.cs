using System;
using System.Collections.Generic;
using UnityEngine;

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

    private float NotesSpeed;
    [SerializeField] private GameObject aBaseNoteObject;

    public void OnEnable()
    {
        NotesSpeed = GManager.Instance.noteSpeed;
        Load("menuettm");
    }

    private void Load(string songName)
    {
        // songNameに対応する譜面データをResourcesから取得し、parseする
        string aSongNamePath = Resources.Load<TextAsset>(songName).ToString();
        NotesInfomation aNotesInfomation = JsonUtility.FromJson<NotesInfomation>(aSongNamePath);

        this.SetMaxScore(aNotesInfomation.Notes.Count);
        this.NotesLineUp(aNotesInfomation);
    }

    /// <summary>
    /// ノーツの情報を参考に、必要な数のノーツを生成し、並べ上げるメソッド
    /// </summary>
    /// <param name="aNotesInfomation">ノーツなどの情報</param>
    private void NotesLineUp(NotesInfomation aNotesInfomation)
    {
        aNotesInfomation.Notes.ForEach ((aNote) => {
            float anInterval = 60 / (aNotesInfomation.BPM * (float) aNote.LPB);
            float aBeatSec = anInterval * aNote.LPB;
            float aTime = (aBeatSec * aNote.num / aNote.LPB) + aNotesInfomation.offset * 0.01f;

            aNote.ActionRequiredTime = aTime;
            this.aNoteList.Add(aNote);

            float aDistance = aTime * NotesSpeed;
            this.aNoteObjects.Add(Instantiate(this.aBaseNoteObject, new Vector3(aNote.block - 1.5f, 0.55f, aDistance), Quaternion.identity));
        });
    }

    ///<summary>
    /// ノーツの数から最大スコアを算出し、設定するメソッド
    ///</summary>
    /// <param name="noteNum">ノーツの数</param>
    private void SetMaxScore(int noteNum)
    {
        GManager.Instance.maxScore = noteNum * 5;
    }
}