using System.Collections.Generic;
using UnityEngine;

public class RunaConector : MonoBehaviour
{
    public LineRenderer linha;
    private List<Transform> runasSelecionadas = new List<Transform>(); // Lista das runas conectadas
    private bool conectando = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector2 posicaoToque = Camera.main.ScreenToWorldPoint(toque.position);

            if (toque.phase == TouchPhase.Began)
            {
                runasSelecionadas.Clear();
                conectando = true;
                linha.positionCount = 0;
            }

            if (toque.phase == TouchPhase.Moved && conectando)
            {
                Collider2D hit = Physics2D.OverlapPoint(posicaoToque);
                if (hit != null && hit.CompareTag("Runa") && !runasSelecionadas.Contains(hit.transform))
                {
                    runasSelecionadas.Add(hit.transform);
                    linha.positionCount = runasSelecionadas.Count;
                    linha.SetPosition(linha.positionCount - 1, hit.transform.position);
                }
            }

            if (toque.phase == TouchPhase.Ended)
            {
                conectando = false;
                VerificarSequencia();
            }
        }
    }

    void VerificarSequencia()
    {
        // Aqui você pode verificar se a sequência de runas está correta
        // Exemplo: comparar runasSelecionadas com uma sequência pré-definida
        if (runasSelecionadas.Count == 4) // Exemplo de um código com 3 runas
        {
            Debug.Log("Feitiço ativado!");
        }
        else
        {
            Debug.Log("Código incorreto, tente novamente.");
        }
    }
}
