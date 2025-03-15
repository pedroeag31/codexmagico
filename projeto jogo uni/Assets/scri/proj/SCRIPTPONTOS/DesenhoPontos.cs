using System.Collections.Generic;
using UnityEngine;

public class DesenhoPontos : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<Transform> pontos; // Lista de pontos do desenho
    private List<Vector3> pontosDesenhados = new List<Vector3>();
    private int indiceAtual = 0;
    private bool desenhando = false;

    void Start()
    {
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posToque.z = 0;

            // Verifica se tocou no primeiro ponto
            if (Vector3.Distance(posToque, pontos[indiceAtual].position) < 0.3f)
            {
                desenhando = true;
                AdicionarPonto(pontos[indiceAtual].position);
            }
        }

        if (Input.GetMouseButton(0) && desenhando)
        {
            Vector3 posToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posToque.z = 0;

            // Se tocou no próximo ponto correto
            if (indiceAtual + 1 < pontos.Count && Vector3.Distance(posToque, pontos[indiceAtual + 1].position) < 0.3f)
            {
                indiceAtual++;
                AdicionarPonto(pontos[indiceAtual].position);

                // Se chegou ao último ponto, finaliza
                if (indiceAtual == pontos.Count - 1)
                {
                    desenhando = false;
                    Debug.Log("Desenho concluído!");
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            desenhando = false;
        }
    }

    void AdicionarPonto(Vector3 ponto)
    {
        pontosDesenhados.Add(ponto);
        lineRenderer.positionCount = pontosDesenhados.Count;
        lineRenderer.SetPositions(pontosDesenhados.ToArray());
    }
}
