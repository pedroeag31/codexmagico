using System.Collections.Generic;
using UnityEngine;

public class RunaConector : MonoBehaviour
{
    public LineRenderer linha;
    private Dictionary<Transform, Transform> paresRunas = new Dictionary<Transform, Transform>(); // Par de runas corretas
    private Transform primeiraRuna; // Runa inicial da conexão
    private bool conectando = false;

    void Start()
    {
        // Definir os pares no Inspector ou adicionar manualmente aqui
        // Exemplo: paresRunas[runas[0]] = runas[1]; (e vice-versa)
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
