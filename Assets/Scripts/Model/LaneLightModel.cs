using System.Collections.Generic;
using UnityEngine;

public class LaneLightModel : MonoBehaviour
{
    private readonly Dictionary<string, Color> ColorDictionary = new Dictionary<string, Color>()
    {
        { "DESK", Color.white },
        { "CLAP", Color.yellow },
        { "PET", Color.cyan },
        { "EMPTY_BOX", new Color(1, 1, 1) }
    };
    private float aLaneAnimationSpeed = 1.5f;
    [SerializeField] private int aLaneNumber;
    private Color aDefaultColor = new Color(0.38f, 0.30f, 0.79f);
    public int LaneNumber
    {
        get
        {
            return this.aLaneNumber;
        }
    }
    private Renderer aRenderer;
    private float aRGBAlfa = 0.2f;
    public void Start()
    {
        this.aRenderer = GetComponent<Renderer>();
        this.PaintRequest(this.aDefaultColor, this.aRGBAlfa);
    }
    public void Update()
    {

        if (this.aRGBAlfa > 0.2f)
        {
            this.aRGBAlfa -= aLaneAnimationSpeed * Time.deltaTime;
            Color aColor = this.aRGBAlfa > 0.3f ? this.aRenderer.material.color : this.aDefaultColor;
            this.PaintRequest(aColor, this.aRGBAlfa);
        }
    }

    private void PaintRequest(Color baseColor, float aRGBAlfa){
        this.aRenderer.material.color = new Color(baseColor.r, baseColor.g, baseColor.b ,aRGBAlfa);
    }

    public void LaneAction(string colorType)
    {
        this.aRGBAlfa = 0.6f;
        this.PaintRequest(ColorDictionary[colorType], this.aRGBAlfa);
    }
}