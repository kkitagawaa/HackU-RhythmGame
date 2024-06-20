using System.Collections.Generic;

///	<summary>
/// NoteEdiitorで生成された譜面構造を、クラス形式で定義するためのシリアライズ可能なクラス。
/// </summary>
[System.Serializable]
public class NotesInfomation
{
	public string name;
	public int maxBlock;
	public int BPM;
	public int offset;
	public NoteData[] notes;
	public List<NoteData> Notes
	{
		get
		{
			return new List<NoteData>(notes);
		}
	}
}