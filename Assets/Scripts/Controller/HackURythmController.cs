using System.Collections.Generic;
using UnityEngine;

public class HackURythmController : MonoBehaviour
{
    private static HackURythmController instance = null;
    public static HackURythmController Instance
    {
        get
        {
            return HackURythmController.instance;
        }
    }
    private JudgeModel aJudge;
    private List<LaneLightModel> aLaneLightList;

    public void Awake()
    {
        if (HackURythmController.instance == null)
        {
            HackURythmController.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void Start()
    {
        this.aJudge = FindObjectsByType<JudgeModel>(FindObjectsSortMode.None)[0];
        this.aLaneLightList = new List<LaneLightModel>(FindObjectsByType<LaneLightModel>(FindObjectsSortMode.None));
    }

    public void JudgeCheck(string inputType)
    {
        // Debug.Log(inputType);
        this.aJudge.Judgement(inputType);
        this.aLaneLightList.ForEach(aLaneLight => aLaneLight.LaneAction(inputType));
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.JudgeCheck("DESK");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            this.JudgeCheck("CLAP");
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
}