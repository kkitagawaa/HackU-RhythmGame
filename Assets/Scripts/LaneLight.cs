using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LaneLight : MonoBehaviour
{
    [SerializeField] private float aLaneAnimationSpeed = 3;
    [SerializeField] private int aLaneNumber;
    public int LaneNumber
    {
        get
        {
            return this.aLaneNumber;
        }
    }
    private Renderer aRenderer;
    private float aRGBAlfa = 0;
    public void Start()
    {
        this.aRenderer = GetComponent<Renderer>();
        this.paintRequest(this.aRenderer, this.aRGBAlfa);
    }
    public void Update()
    {

        if (!(this.aRGBAlfa <= 0))
        {
            this.aRGBAlfa -= aLaneAnimationSpeed * Time.deltaTime;
            this.paintRequest(this.aRenderer, this.aRGBAlfa);
        }
    }

    private void paintRequest(Renderer aRenderer, float aRGBAlfa){
        Color aBaseColor = aRenderer.material.color;
        aRenderer.material.color = new Color(aBaseColor.r, aBaseColor.g, aBaseColor.b ,aRGBAlfa);

    }

    public void LaneAction()
    {
        this.aRGBAlfa = 0.3f;
    }
}