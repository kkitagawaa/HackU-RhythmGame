using UnityEngine;

public class Notes : MonoBehaviour
{
    private float aNoteSpeed = 8;

    public void Start()
    {
        this.aNoteSpeed = GameManager.Instance.NoteSpeed;
    }

    public void Update()
    {
        if (GameManager.Instance.IsGameStart)
            this.transform.position -= this.transform.forward * Time.deltaTime * this.aNoteSpeed;

    }
}