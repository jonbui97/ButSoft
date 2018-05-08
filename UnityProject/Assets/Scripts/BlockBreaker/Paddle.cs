using UnityEngine;
public class Paddle : MonoBehaviour
{
    public float maxX;
    public float minX;

    void Update()
    {
        Vector3 paddlePosition = new Vector3();
        paddlePosition = this.transform.position;

        float mousePositionInBloks = Input.mousePosition.x / Screen.width * 16 - 8f;
        paddlePosition.x = Mathf.Clamp(mousePositionInBloks, minX, maxX);
        this.transform.position = paddlePosition;
    }
}