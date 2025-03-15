using System.Collections.Generic;
using UnityEngine;

public class RunaConector : MonoBehaviour
{
    public GameObject linhaPrefab; // Prefab de um LineRenderer para criar novas linhas
    public Transform runa1, runa2, runa3, runa4, runa5, runa6; // Runas definidas no Inspector

    private Dictionary<Transform, Transform> paresRunas = new Dictionary<Transform, Transform>(); // Pares de runas
    private Transform primeiraRuna; // Primeira runa tocada
    private bool conectando = false;
    private LineRenderer linhaAtual; // Linha que está sendo desenhada no momento
    private List<LineRenderer> linhasCriadas = new List<LineRenderer>(); // Lista de todas as conexões feitas

    void Start()
    {
        // Definir os pares de runas
        paresRunas.Add(runa1, runa2);
        paresRunas.Add(runa2, runa1);
        paresRunas.Add(runa3, runa4);
        paresRunas.Add(runa4, runa3);
        paresRunas.Add(runa5, runa6);
        paresRunas.Add(runa6, runa5);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector2 posicaoToque = Camera.main.ScreenToWorldPoint(toque.position);

            if (toque.phase == TouchPhase.Began)
            {
                Collider2D hit = Physics2D.OverlapPoint(posicaoToque);
                if (hit != null && hit.CompareTag("Runa"))
                {
                    if (paresRunas.ContainsKey(hit.transform))
                    {
                        primeiraRuna = hit.transform;
                        conectando = true;

                        // Criar uma nova linha e configurar
                        GameObject novaLinha = Instantiate(linhaPrefab);
                        linhaAtual = novaLinha.GetComponent<LineRenderer>();
                        linhaAtual.positionCount = 2;
                        linhaAtual.startWidth = 0.1f;
                        linhaAtual.endWidth = 0.1f;
                        linhaAtual.SetPosition(0, primeiraRuna.position);

                        // Adiciona a nova linha à lista
                        linhasCriadas.Add(linhaAtual);
                    }
                }
            }

            if (toque.phase == TouchPhase.Moved && conectando)
            {
                linhaAtual.SetPosition(1, posicaoToque);
            }

            if (toque.phase == TouchPhase.Ended)
            {
                Collider2D hit = Physics2D.OverlapPoint(posicaoToque);
                if (hit != null && hit.CompareTag("Runa") && hit.transform != primeiraRuna)
                {
                    if (paresRunas.ContainsKey(primeiraRuna) && paresRunas[primeiraRuna] == hit.transform)
                    {
                        linhaAtual.SetPosition(1, hit.transform.position);
                        Debug.Log("Feitiço ativado!");
                    }
                    else
                    {
                        Debug.Log("Erro! Conexão inválida.");
                        Destroy(linhaAtual.gameObject); // Remove a linha errada
                    }
                }
                else
                {
                    Destroy(linhaAtual.gameObject); // Se não tocou em outra runa, apaga a linha
                }

                conectando = false;
            }
        }
    }
}
