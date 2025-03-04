using System.Collections.Generic;
using UnityEngine;

public class RunaConector : MonoBehaviour
{
    public LineRenderer linha;
    public Transform runa1, runa2, runa3, runa4, runa5, runa6; // Defina no Inspector

    private Dictionary<Transform, Transform> paresRunas = new Dictionary<Transform, Transform>(); // Pares corretos
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

        linha.positionCount = 0; // Começa sem nenhuma linha desenhada
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector2 posicaoToque = Camera.main.ScreenToWorldPoint(toque.position);

            if (toque.phase == TouchPhase.Began)
            {
                linha.positionCount = 0; // Limpa a linha antes de começar uma nova conexão
                Collider2D hit = Physics2D.OverlapPoint(posicaoToque);
                if (hit != null && hit.CompareTag("Runa"))
                {
                    if (paresRunas.ContainsKey(hit.transform)) // Se for uma runa válida
                    {
                        primeiraRuna = hit.transform;
                        conectando = true;
                        linha.positionCount = 1;
                        linha.SetPosition(0, primeiraRuna.position);
                    }
                }
            }

            if (toque.phase == TouchPhase.Moved && conectando)
            {
                Collider2D hit = Physics2D.OverlapPoint(posicaoToque);
                if (hit != null && hit.CompareTag("Runa") && hit.transform != primeiraRuna)
                {
                    if (paresRunas.ContainsKey(primeiraRuna) && paresRunas[primeiraRuna] == hit.transform)
                    {
                        linha.positionCount = 2;
                        linha.SetPosition(1, hit.transform.position);
                        Debug.Log("Conexão correta! Feitiço ativado!");
                    }
                    else
                    {
                        Debug.Log("Erro! Conexão inválida.");
                        linha.positionCount = 0; // Apaga a linha apenas se for incorreto
                    }
                    conectando = false;
                }
            }

            if (toque.phase == TouchPhase.Ended)
            {
                conectando = false;
            }
        }
    }
}
