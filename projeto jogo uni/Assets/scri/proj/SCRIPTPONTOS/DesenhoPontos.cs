using UnityEngine;
using UnityEngine.UI; // Para manipular UI
using System.Collections.Generic;

public class DesenhoPontos : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<Transform> pontos;
    public GameObject telaVitoria; // Tela de vitória
    public GameObject telaDerrota; // Tela de derrota
    public Text timerText; // Texto do tempo
    public float tempoLimite = 60f; // Tempo máximo

    private List<Vector3> pontosDesenhados = new List<Vector3>();
    private int indiceAtual = 0;
    private bool desenhando = false;
    private bool jogoFinalizado = false;
    private float tempoRestante;

    void Start()
    {
        lineRenderer.positionCount = 0;
        telaVitoria.SetActive(false);
        telaDerrota.SetActive(false);
        tempoRestante = tempoLimite;
    }

    void Update()
    {
        if (jogoFinalizado) return;

        // Atualiza o tempo restante
        tempoRestante -= Time.deltaTime;
        timerText.text = "Tempo: " + Mathf.Ceil(tempoRestante).ToString();

        if (tempoRestante <= 0)
        {
            PerderJogo();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posToque = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posToque.z = 0;

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

            if (indiceAtual + 1 < pontos.Count && Vector3.Distance(posToque, pontos[indiceAtual + 1].position) < 0.3f)
            {
                indiceAtual++;
                AdicionarPonto(pontos[indiceAtual].position);

                if (indiceAtual == pontos.Count - 1)
                {
                    desenhando = false;
                    MostrarTelaVitoria();
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

    void MostrarTelaVitoria()
    {
        jogoFinalizado = true;
        telaVitoria.SetActive(true);
    }

    void PerderJogo()
    {
        jogoFinalizado = true;
        telaDerrota.SetActive(true);
    }

    public void JogarNovamente()
    {
        telaVitoria.SetActive(false);
        telaDerrota.SetActive(false);
        lineRenderer.positionCount = 0;
        pontosDesenhados.Clear();
        indiceAtual = 0;
        tempoRestante = tempoLimite;
        jogoFinalizado = false;
    }
}
