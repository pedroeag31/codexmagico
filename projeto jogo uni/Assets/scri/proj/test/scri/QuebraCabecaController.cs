using UnityEngine;

public class QuebraCabecaController : MonoBehaviour
{
    public static QuebraCabecaController Instance;
    public GameObject telaVitoria; // Painel de vitória
    private ArrastarPeca[] pecas;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        telaVitoria.SetActive(false);
        pecas = FindObjectsOfType<ArrastarPeca>();
    }

    public void VerificarVitoria()
    {
        foreach (ArrastarPeca peca in pecas)
        {
            if (peca.GetComponent<CanvasGroup>().blocksRaycasts)
            {
                return;
            }
        }

        telaVitoria.SetActive(true);
    }
}
