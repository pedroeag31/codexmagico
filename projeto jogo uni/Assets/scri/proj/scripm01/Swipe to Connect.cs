using System.Collections.Generic;
using UnityEngine;

public class RunaConector : MonoBehaviour
{
    public LineRenderer linha;
    public Transform runa1, runa2, runa3, runa4, runa5, runa6; // Defina as runas no Inspector

    private Dictionary<Transform, Transform> paresRunas = new Dictionary<Transform, Transform>(); // Pares de runas
    private Transform primeiraRuna; // Runa inicial da conexão
    private bool conectando = false;

    void Start()
    {
        // Definir os pares no início do jogo
        paresRunas.Add(runa1, runa2);
        paresRunas.Add(runa2, runa1);
        paresRunas.Add(runa3, runa4);
        paresRunas.Add(runa4, runa3);
        paresRunas.Add(runa5, runa6);
        paresRunas.Add(runa6, runa5);

        linha.positionCount = 0; // Iniciar sem nenhuma linha desenhada
        linha.startWidth = 0.1f; // Largura inicial da linha
        linha.endWidth = 0.1f; // Largura final da linha
        linha.material = new Material(Shader.Find("Sprites/Default")); // Defina um material para o LineRenderer, caso não tenha
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector2 posicaoToque = Camera.main.ScreenToWorldPoint(toque.position); // Converter para coordenadas do mundo

            if (toque.phase == TouchPhase.Began)
            {
                // Limpa a linha ao começar um novo toque
                linha.positionCount = 0; // Resetando a linha

                // Verifica se o toque está sobre uma runa
                Collider2D hit = Physics2D.OverlapPoint(posicaoToque);
                if (hit != null && hit.CompareTag("Runa"))
                {
                    if (paresRunas.ContainsKey(hit.transform))
                    {
                        primeiraRuna = hit.transform;
                        conectando = true;

                        // Inicia a linha da runa inicial
                        linha.positionCount = 2; // Linha terá dois pontos: o começo e o fim
                        linha.SetPosition(0, primeiraRuna.position); // Posição inicial da linha
                    }
                }
            }

            if (toque.phase == TouchPhase.Moved && conectando)
            {
                // A linha se ajusta conforme o dedo se move
                linha.SetPosition(1, posicaoToque); // A linha vai até a posição atual do toque
            }

            if (toque.phase == TouchPhase.Ended)
            {
                // Verifica se a linha conecta as runas corretamente
                Collider2D hit = Physics2D.OverlapPoint(posicaoToque);
                if (hit != null && hit.CompareTag("Runa") && hit.transform != primeiraRuna)
                {
                    // Verifica se a runa tocada faz parte do par correto
                    if (paresRunas.ContainsKey(primeiraRuna) && paresRunas[primeiraRuna] == hit.transform)
                    {
                        // Conectou corretamente
                        linha.SetPosition(1, hit.transform.position); // Finaliza a linha na segunda runa
                        Debug.Log("Feitiço ativado!");
                    }
                    else
                    {
                        // Se a conexão for errada, apaga a linha
                        Debug.Log("Erro! Conexão inválida.");
                        linha.positionCount = 0;
                    }
                }

                conectando = false; // Finaliza a conexão
            }
        }
    }
}
