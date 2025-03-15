using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nomeCena; // Nome da cena a ser carregada ao interagir com o portal
    public float distanciaDeInteracao = 0.5f; // Dist�ncia de intera��o do jogador com o portal

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colis�o detectada com: " + other.gameObject.name); // Mensagem para depura��o

        if (other.CompareTag("Jogador")) // Verifica se o objeto tem a tag "Jogador"
        {
            Debug.Log("Jogador entrou no portal: " + nomeCena); // Mensagem de depura��o
            InteragirComPortal(other.transform); // Chama a fun��o para interagir com o portal
        }
    }

    private void InteragirComPortal(Transform jogadorTransform)
    {
        float distancia = Vector2.Distance(jogadorTransform.position, transform.position); // Calcula a dist�ncia real
        Debug.Log("Dist�ncia entre Jogador e Portal: " + distancia); // Exibe a dist�ncia no Console

        if (distancia < distanciaDeInteracao) // Verifica se o jogador est� dentro da �rea de intera��o
        {
            Debug.Log("Jogador est� perto o suficiente do portal. Carregando cena..."); // Mensagem de depura��o
            CarregarCena(); // Carrega a cena
        }
        else
        {
            Debug.Log("Jogador muito longe do portal."); // Mensagem de depura��o
        }
    }

    private void CarregarCena()
    {
        if (!string.IsNullOrEmpty(nomeCena)) // Verifica se h� um nome de cena v�lido
        {
            Debug.Log("Carregando a cena: " + nomeCena); // Mensagem de depura��o
            SceneManager.LoadScene(nomeCena); // Carrega a cena correspondente
        }
        else
        {
            Debug.LogWarning("Nome da cena est� vazio!"); // Mensagem de aviso no console
        }
    }
}
