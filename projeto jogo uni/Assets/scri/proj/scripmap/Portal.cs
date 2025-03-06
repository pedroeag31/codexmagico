using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nomeCena; // Nome da cena a ser carregada ao interagir com o portal
    public float distanciaDeInteracao = 0.5f; // Dist�ncia de intera��o

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colis�o detectada com: " + other.gameObject.name); // TESTE

        if (other.CompareTag("Jogador")) // Verifica se o objeto tem a tag "Jogador"
        {
            Debug.Log("Jogador entrou no portal: " + nomeCena); // TESTE
            InteragirComPortal(other.transform);
        }
    }

    private void InteragirComPortal(Transform jogadorTransform)
    {
        if (Vector2.Distance(jogadorTransform.position, transform.position) < distanciaDeInteracao)
        {
            Debug.Log("Jogador est� perto o suficiente do portal. Carregando cena..."); // TESTE
            CarregarCena();
        }
        else
        {
            Debug.Log("Jogador muito longe do portal."); // TESTE
        }
    }

    private void CarregarCena()
    {
        if (!string.IsNullOrEmpty(nomeCena))
        {
            Debug.Log("Carregando a cena: " + nomeCena); // TESTE
            SceneManager.LoadScene(nomeCena);
        }
        else
        {
            Debug.LogWarning("Nome da cena est� vazio!"); // TESTE
        }
    }
}
