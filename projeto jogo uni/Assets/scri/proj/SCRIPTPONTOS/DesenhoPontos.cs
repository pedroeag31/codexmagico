using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DesenhoPontos : MonoBehaviour
{
    [Header("Sequências de Desenhos")]
    public List<SequenciaDePontos> desenhos;
    private int desenhoAtual = 0;

    [Header("Line Renderer e UI")]
    public LineRenderer lineRenderer;
    public Text timerText;
    public GameObject telaVitoria;
    public GameObject telaDerrota;

    [Header("Tempo")]
    public float tempoLimite = 60f;
    private float tempoRestante;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip somAcerto;
    public AudioClip somErro;
    public AudioClip somPagina;

    private List<Transform> pontos = new List<Transform>();
    private List<Vector3> pontosDesenhados = new List<Vector3>();
    private int indiceAtual = 0;
    private bool desenhando = false;
    private bool jogoFinalizado = false;

    void Start()
    {
        telaVitoria.SetActive(false);
        telaDerrota.SetActive(false);
        AtivarDesenho(desenhoAtual);
    }

    void Update()
    {
        if (jogoFinalizado) return;

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
                audioSource.PlayOneShot(somAcerto);
            }
            else
            {
                audioSource.PlayOneShot(somErro);
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
                audioSource.PlayOneShot(somAcerto);

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
        Invoke("ProximaPagina", 2f); // Espera 2 segundos e vai pro próximo desenho
    }

    void PerderJogo()
    {
        jogoFinalizado = true;
        telaDerrota.SetActive(true);
    }

    void ProximaPagina()
    {
        telaVitoria.SetActive(false);
        if (desenhoAtual + 1 < desenhos.Count)
        {
            desenhoAtual++;
            audioSource.PlayOneShot(somPagina);
            AtivarDesenho(desenhoAtual);
        }
        else
        {
            Debug.Log("🎉 Todos os desenhos completados!");
        }
    }

    public void JogarNovamente()
    {
        desenhoAtual = 0;
        telaVitoria.SetActive(false);
        telaDerrota.SetActive(false);
        AtivarDesenho(desenhoAtual);
    }

    void AtivarDesenho(int index)
    {
        // Ativa apenas o desenho atual
        for (int i = 0; i < desenhos.Count; i++)
            desenhos[i].gameObject.SetActive(i == index);

        // Limpa e carrega a nova sequência de pontos
        pontos.Clear();
        SequenciaDePontos seq = desenhos[index].GetComponent<SequenciaDePontos>();
        if (seq != null)
        {
            pontos = new List<Transform>(seq.pontos); // Usa a ordem definida no Inspetor
        }

        // Reseta variáveis
        lineRenderer.positionCount = 0;
        pontosDesenhados.Clear();
        indiceAtual = 0;
        tempoRestante = tempoLimite;
        jogoFinalizado = false;
    }

}