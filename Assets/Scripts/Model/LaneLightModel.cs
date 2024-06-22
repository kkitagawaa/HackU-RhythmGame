using System.Collections.Generic;
using UnityEngine;

public class LaneLightModel : MonoBehaviour
{
    private static readonly Dictionary<string, Color> ColorDictionary = new Dictionary<string, Color>()
    {
        { "DESK", Color.white },
        { "CLAP", Color.yellow},
        { "PET", Color.cyan },
        { "EMPTY_BOX", new Color(1, 1, 1) }
    };
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
        this.paintRequest(this.aRenderer.material.color, this.aRGBAlfa);
    }
    public void Update()
    {

        if (!(this.aRGBAlfa <= 0))
        {
            this.aRGBAlfa -= aLaneAnimationSpeed * Time.deltaTime;
            this.paintRequest(this.aRenderer.material.color, this.aRGBAlfa);
        }
    }

    private void paintRequest(Color baseColor, float aRGBAlfa){
        this.aRenderer.material.color = new Color(baseColor.r, baseColor.g, baseColor.b ,aRGBAlfa);
    }

    public void LaneAction(string colorType)
    {
        this.aRGBAlfa = 0.3f;
        this.paintRequest(ColorDictionary[colorType], this.aRGBAlfa);
    }
}