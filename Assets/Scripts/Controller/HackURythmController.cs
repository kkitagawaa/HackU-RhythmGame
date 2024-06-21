using System;
using System.Collections.Generic;
using UnityEngine;

public class HackURythmController : MonoBehaviour
{
    private JudgeModel aJudge;
    private LaneLightModel[] aLaneLightList;
    public void Start()
    {
        this.aJudge = FindObjectsByType<JudgeModel>(FindObjectsSortMode.None)[0];
        this.aLaneLightList = FindObjectsByType<LaneLightModel>(FindObjectsSortMode.None);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.aJudge.Judgement(0);
            this.findLaneLight(0).LaneAction();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            this.aJudge.Judgement(1);
            this.findLaneLight(1).LaneAction();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            this.aJudge.Judgement(2);
            this.findLaneLight(2).LaneAction();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            this.aJudge.Judgement(3);
            this.findLaneLight(3).LaneAction();
        }
        else
            this.aJudge.Judgement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartPlay();
        }
    }

    private LaneLightModel findLaneLight(int aLaneNumber)
    {
        foreach (LaneLightModel aLaneLight in this.aLaneLightList)
        {
            if (aLaneLight.LaneNumber == aLaneNumber)
                return aLaneLight;
        }
        return null;
    }
}