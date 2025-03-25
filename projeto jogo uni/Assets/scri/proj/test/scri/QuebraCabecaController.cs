using UnityEngine;
using UnityEngine.UI;
public class QuebraCabecaController : MonoBehaviour
{
    public static QuebraCabecaController Instance;
    public GameObject telaVitoria; // Painel de vitória
    private ArrastarPeca1[] pecas;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        telaVitoria.SetActive(false);
        pecas = FindObjectsOfType<ArrastarPeca1>();
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

        telaVitoria.SetActive(true);
    }
}
