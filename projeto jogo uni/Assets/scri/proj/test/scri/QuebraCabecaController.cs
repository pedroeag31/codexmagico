using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Necessário para usar IEnumerator

public class QuebraCabecaController : MonoBehaviour
{
    public static QuebraCabecaController Instance;
    public GameObject telaVitoria;
    public GameObject telaDerrota;
    public Text textoTemporizador; // UI para exibir o tempo
    private ArrastarPeca1[] pecas;
    private float tempoRestante = 60f;
    private bool jogoAtivo = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        telaVitoria.SetActive(false);
        telaDerrota.SetActive(false);
        pecas = FindObjectsOfType<ArrastarPeca1>();

        StartCoroutine(Temporizador());
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

        jogoAtivo = false;
        telaVitoria.SetActive(true);
        StopCoroutine(Temporizador());
    }

    IEnumerator Temporizador()
    {
        while (tempoRestante > 0 && jogoAtivo)
        {
            textoTemporizador.text = "Tempo: " + Mathf.CeilToInt(tempoRestante).ToString();
            yield return new WaitForSeconds(1f);
            tempoRestante--;
        }

        if (tempoRestante <= 0)
        {
            jogoAtivo = false;
            telaDerrota.SetActive(true);
        }
    }
}
