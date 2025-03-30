using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuebraCabecaController : MonoBehaviour
{
    public static QuebraCabecaController Instance;

    public GameObject telaVitoria; // Painel de vitória
    public GameObject telaDerrota; // Painel de derrota
    public GameObject[] paginasQuebraCabeca; // Lista de páginas do quebra-cabeça
    public Text temporizadorTexto; // UI do tempo
    public float tempoMaximo = 60f; // Tempo limite em segundos

    private int paginaAtual = 0;
    private ArrastarPeca1[] pecas;
    private float tempoRestante;
    private bool jogoAtivo = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        telaVitoria.SetActive(false);
        telaDerrota.SetActive(false);
        AtivarPagina(paginaAtual);
        StartCoroutine(Temporizador());
    }

    void AtivarPagina(int index)
    {
        for (int i = 0; i < paginasQuebraCabeca.Length; i++)
        {
            paginasQuebraCabeca[i].SetActive(i == index);
        }

        pecas = paginasQuebraCabeca[paginaAtual].GetComponentsInChildren<ArrastarPeca1>();
        tempoRestante = tempoMaximo;
        jogoAtivo = true;
    }

    IEnumerator Temporizador()
    {
        while (tempoRestante > 0 && jogoAtivo)
        {
            temporizadorTexto.text = "Tempo: " + Mathf.Ceil(tempoRestante);
            yield return new WaitForSeconds(1f);
            tempoRestante--;
        }

        if (tempoRestante <= 0)
        {
            Derrota();
        }
    }

    public void VerificarVitoria()
    {
        foreach (ArrastarPeca1 peca in pecas)
        {
            if (peca.GetComponent<CanvasGroup>().blocksRaycasts)
            {
                return;
            }
        }

        StartCoroutine(TrocarPagina());
    }

    IEnumerator TrocarPagina()
    {
        jogoAtivo = false;
        telaVitoria.SetActive(true);
        yield return new WaitForSeconds(2f);
        telaVitoria.SetActive(false);

        if (paginaAtual < paginasQuebraCabeca.Length - 1)
        {
            paginaAtual++;
            AtivarPagina(paginaAtual);
            StartCoroutine(Temporizador()); // Reinicia o tempo
        }
        else
        {
            Debug.Log("Todos os quebra-cabeças foram completados!");
        }
    }

    void Derrota()
    {
        jogoAtivo = false;
        telaDerrota.SetActive(true);
        Debug.Log("Tempo esgotado! Você perdeu.");
    }
}
