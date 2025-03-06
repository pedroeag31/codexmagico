using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nomeCena; // Nome da cena a ser carregada ao interagir com o portal
    public float distanciaDeInteracao = 0.5f; // Distância de interação

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu com o portal tem a tag "Jogador"
        if (other.CompareTag("Jogador"))
        {
            // O jogador pode interagir com o portal
            InteragirComPortal(other.transform);
        }
    }

    private void InteragirComPortal(Transform jogadorTransform)
    {
        // Verifica se o jogador está dentro da distância de interação
        if (Vector2.Distance(jogadorTransform.position, transform.position) < distanciaDeInteracao)
        {
            CarregarCena(); // Carrega a cena associada ao portal
        }
    }

    private void CarregarCena()
    {
        if (!string.IsNullOrEmpty(nomeCena))
        {
            SceneManager.LoadScene(nomeCena); // Carrega a cena associada ao portal
        }
    }
}
