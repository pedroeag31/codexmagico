using UnityEngine;

public class TesteToque : MonoBehaviour
{
    void Update()
    {
        // Detecta toque na tela (mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            DetectarToque(touchPosition);
        }

        // Detecta clique do mouse (Editor / PC)
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DetectarToque(clickPosition);
        }
    }

    void DetectarToque(Vector2 position)
    {
        // Teste de debug para verificar a posição do toque
        Debug.Log($"📍 Toque/Clique na tela → Convertido para mundo: {position}");

        // Teste de Raycast para detectar objetos
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, Mathf.Infinity, ~0);

        if (hit.collider != null)
        {
            Debug.Log($"✅ Toque detectado no objeto: {hit.collider.gameObject.name}");
        }
        else
        {
            Debug.Log("❌ Nenhum objeto tocado!");
        }

        // Teste de OverlapPoint (método alternativo)
        Collider2D hitCollider = Physics2D.OverlapPoint(position);
        if (hitCollider != null)
        {
            Debug.Log($"✅ (OverlapPoint) Toque detectado no objeto: {hitCollider.gameObject.name}");
        }

        // Desenhar um Debug Ray para visualizar onde está sendo testado
        Debug.DrawRay(position, Vector2.up * 0.5f, Color.red, 2f);
    }
}
