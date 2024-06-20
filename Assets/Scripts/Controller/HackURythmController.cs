using System;
using System.Collections.Generic;
using UnityEngine;

public class HackURythmController : MonoBehaviour
{
    private JudgeModel aJudge;
    public void Start()
    {
        this.aJudge = FindObjectsByType<JudgeModel>(FindObjectsSortMode.None)[0];
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            this.aJudge.Judgement(0);
        else if (Input.GetKeyDown(KeyCode.F))
            this.aJudge.Judgement(1);
        else if (Input.GetKeyDown(KeyCode.J))
            this.aJudge.Judgement(2);
        else if (Input.GetKeyDown(KeyCode.K))
            this.aJudge.Judgement(3);
        else
            this.aJudge.Judgement();
    }
}