using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nomeCena; // Nome da cena a ser carregada ao interagir com o portal
    public float distanciaDeInteracao = 0.5f; // Distância de interação do jogador com o portal

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisão detectada com: " + other.gameObject.name); // Mensagem para depuração

        if (other.CompareTag("Jogador")) // Verifica se o objeto tem a tag "Jogador"
        {
            Debug.Log("Jogador entrou no portal: " + nomeCena); // Mensagem de depuração
            InteragirComPortal(other.transform); // Chama a função para interagir com o portal
        }
    }

    private void InteragirComPortal(Transform jogadorTransform)
    {
        float distancia = Vector2.Distance(jogadorTransform.position, transform.position); // Calcula a distância real
        Debug.Log("Distância entre Jogador e Portal: " + distancia); // Exibe a distância no Console

        if (distancia < distanciaDeInteracao) // Verifica se o jogador está dentro da área de interação
        {
            Debug.Log("Jogador está perto o suficiente do portal. Carregando cena..."); // Mensagem de depuração
            CarregarCena(); // Carrega a cena
        }
        else
        {
            Debug.Log("Jogador muito longe do portal."); // Mensagem de depuração
        }
    }

    private void CarregarCena()
    {
        if (!string.IsNullOrEmpty(nomeCena)) // Verifica se há um nome de cena válido
        {
            Debug.Log("Carregando a cena: " + nomeCena); // Mensagem de depuração
            SceneManager.LoadScene(nomeCena); // Carrega a cena correspondente
        }
        else
        {
            Debug.LogWarning("Nome da cena está vazio!"); // Mensagem de aviso no console
        }
    }
}
