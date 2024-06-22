using System.Collections.Generic;

///	<summary>
/// NoteEdiitorで生成された譜面構造を、クラス形式で定義するためのシリアライズ可能なクラス。
/// ファイルパースに使うため、侵襲可能属性が存在する。
/// </summary>
[System.Serializable]
public class NoteData
{
    public int type;
    public int num;
    public int block;
    public int LaneNumber
    {
        get
        {
            return block;
        }
    }
    public int LPB;
    private float aActionRequiredTime;
    public float ActionRequiredTime
    {
        get
        {
            return aActionRequiredTime;
        }
        set
        {
            aActionRequiredTime = value;
        }
    }
    public NoteData[] notes;
    public List<NoteData> Notes
    {
        get
        {
            return new List<NoteData>(notes);
        }
    }
}