using UnityEngine;

public class ArrastarPeca : MonoBehaviour
{
    private bool arrastando = false;
    private bool podeMover = true;
    private Vector3 offset;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log("🟢 Tocou em: " + hit.collider.gameObject.name);
                }
                else
                {
                    Debug.Log("❌ Nada foi tocado!");
                }
            }
        }
    }


    public void BloquearMovimento()
    {
        podeMover = false;
    }
}
