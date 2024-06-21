using System;
using System.Collections.Generic;
using UnityEngine;

public class HackURythmController : MonoBehaviour
{
    private JudgeModel aJudge;
    private LaneLight[] aLaneLightList;
    public void Start()
    {
        this.aJudge = FindObjectsByType<JudgeModel>(FindObjectsSortMode.None)[0];
        this.aLaneLightList = FindObjectsByType<LaneLight>(FindObjectsSortMode.None);
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
    }

    private LaneLight findLaneLight(int aLaneNumber)
    {
        foreach (LaneLight aLaneLight in this.aLaneLightList)
        {
            if (aLaneLight.LaneNumber == aLaneNumber)
                return aLaneLight;
        }
        return null;
    }
}