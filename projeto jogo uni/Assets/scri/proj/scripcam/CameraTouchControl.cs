using UnityEngine;

public class CameraTouchControl : MonoBehaviour
{
    private Vector2 touchStart;
    public float dragSpeed = 0.01f; // Velocidade do arrasto

    // Limites da câmera (ajuste no Inspector)
    public float minX = -10f, maxX = 10f, minY = -10f, maxY = 10f;

    void Update()
    {
        if (Input.touchCount == 1) // Apenas um dedo tocando na tela
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 direction = touchStart - touch.position;
                Vector3 newPosition = transform.position + new Vector3(direction.x * dragSpeed, direction.y * dragSpeed, 0);

                // Aplica os limites para impedir que a câmera saia do quebra-cabeça
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                transform.position = newPosition;
                touchStart = touch.position;
            }
        }
    }
}
