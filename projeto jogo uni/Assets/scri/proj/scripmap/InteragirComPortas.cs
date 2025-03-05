using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar as cenas dos mini games
using System.Collections.Generic; // Para usar listas

public class InteragirComPortas : MonoBehaviour
{
    public List<string> cenasMiniGames; // Lista das cenas dos mini games
    public float distanciaDeInteracao = 0.5f; // Dist�ncia para detectar intera��o
    private MovimentoJogador movimentoJogador; // Refer�ncia ao script MovimentoJogador
    private int cenaIndex = 0; // �ndice para selecionar qual cena carregar
    private Collider2D portalCollider; // Refer�ncia ao Collider2D do portal

    void Start()
    {
        // Obt�m a refer�ncia ao script MovimentoJogador
        movimentoJogador = FindObjectOfType<MovimentoJogador>();

        // Obt�m o Collider2D do portal para detectar a intera��o
        portalCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (movimentoJogador != null && cenasMiniGames.Count > 0 && portalCollider != null) // Verifica se h� cenas na lista e o portal tem um collider
        {
            // Verifica se o jogador est� perto do portal e se o toque est� sobre o portal
            if (Vector2.Distance(transform.position, movimentoJogador.Destino) < distanciaDeInteracao)
            {
                // Verifica se o jogador tocou na �rea do portal usando o Collider2D
                if (portalCollider.bounds.Contains(movimentoJogador.Destino))
                {
                    // Se o �ndice estiver dentro do alcance das cenas, carrega a cena
                    if (cenaIndex < cenasMiniGames.Count)
                    {
                        CarregarMiniGame(cenaIndex);
                        cenaIndex++; // Incrementa o �ndice para carregar a pr�xima cena
                    }
                }
            }
        }
    }

    void CarregarMiniGame(int index)
    {
        // Carrega a cena correspondente ao �ndice da lista
        SceneManager.LoadScene(cenasMiniGames[index]);
    }
}
