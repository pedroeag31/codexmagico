using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar as cenas dos mini games
using System.Collections.Generic; // Para usar listas

public class InteragirComPortas : MonoBehaviour
{
    public List<string> cenasMiniGames; // Lista das cenas dos mini games
    public float distanciaDeInteracao = 0.5f; // Distância para detectar interação
    private MovimentoJogador movimentoJogador; // Referência ao script MovimentoJogador
    private int cenaIndex = 0; // Índice para selecionar qual cena carregar
    private Collider2D portalCollider; // Referência ao Collider2D do portal

    void Start()
    {
        // Obtém a referência ao script MovimentoJogador
        movimentoJogador = FindObjectOfType<MovimentoJogador>();

        // Obtém o Collider2D do portal para detectar a interação
        portalCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (movimentoJogador != null && cenasMiniGames.Count > 0 && portalCollider != null) // Verifica se há cenas na lista e o portal tem um collider
        {
            // Verifica se o jogador está perto do portal e se o toque está sobre o portal
            if (Vector2.Distance(transform.position, movimentoJogador.Destino) < distanciaDeInteracao)
            {
                // Verifica se o jogador tocou na área do portal usando o Collider2D
                if (portalCollider.bounds.Contains(movimentoJogador.Destino))
                {
                    // Se o índice estiver dentro do alcance das cenas, carrega a cena
                    if (cenaIndex < cenasMiniGames.Count)
                    {
                        CarregarMiniGame(cenaIndex);
                        cenaIndex++; // Incrementa o índice para carregar a próxima cena
                    }
                }
            }
        }
    }

    void CarregarMiniGame(int index)
    {
        // Carrega a cena correspondente ao índice da lista
        SceneManager.LoadScene(cenasMiniGames[index]);
    }
}
