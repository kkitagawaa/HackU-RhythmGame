using System.Collections.Generic;
using UnityEngine;

public class HackURythmController : MonoBehaviour
{
    private JudgeModel aJudge;
    private List<LaneLightModel> aLaneLightList;
    public void Start()
    {
        this.aJudge = FindObjectsByType<JudgeModel>(FindObjectsSortMode.None)[0];
        this.aLaneLightList = new List<LaneLightModel>(FindObjectsByType<LaneLightModel>(FindObjectsSortMode.None));
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.aJudge.Judgement("DESK");
            this.aLaneLightList.ForEach(aLaneLight => aLaneLight.LaneAction("DESK"));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.aJudge.Judgement("CLAP");
            this.aLaneLightList.ForEach(aLaneLight => aLaneLight.LaneAction("CLAP"));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.aJudge.Judgement("PET");
            this.aLaneLightList.ForEach(aLaneLight => aLaneLight.LaneAction("PET"));
        }
        
        this.aJudge.Judgement(null);

        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameStart)
        {
            GameManager.Instance.StartPlay();
        }
    }

    // private LaneLightModel findLaneLight(int aLaneNumber)
    // {
    //     foreach (LaneLightModel aLaneLight in this.aLaneLightList)
    //     {
    //         if (aLaneLight.LaneNumber == aLaneNumber)
    //             return aLaneLight;
    //     }
    //     return null;
    // }
}