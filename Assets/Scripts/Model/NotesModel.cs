using UnityEngine;

public class Notes : MonoBehaviour
{
    private float aNoteSpeed;

    public void Start()
    {
        this.aNoteSpeed = GameManager.Instance.NoteSpeed;
    }

    public void Update()
    {
        if (GameManager.Instance.Start)
            this.transform.position -= this.transform.forward * Time.deltaTime * this.aNoteSpeed;

    }
}