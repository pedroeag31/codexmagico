using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrastarPeca1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 posicaoInicial;
    public Transform slotCorreto;
    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        posicaoInicial = rectTransform.anchoredPosition; // Guarda a posição inicial
    }

    public void DefinirSlotCorreto(Transform slot)
    {
        slotCorreto = slot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.7f;  // Deixa a peça um pouco transparente
        canvasGroup.blocksRaycasts = false; // Permite arrastar sobre outros elementos UI
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Atualiza a posição usando o toque
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Verifica se a peça está no slot correto
        float distancia = Vector2.Distance(rectTransform.anchoredPosition, slotCorreto.GetComponent<RectTransform>().anchoredPosition);

        Debug.Log($"Peca: {gameObject.name} | Distancia do slot: {distancia}");

        if (distancia < 50f)
        {
            rectTransform.anchoredPosition = slotCorreto.GetComponent<RectTransform>().anchoredPosition;
            canvasGroup.blocksRaycasts = false; // Impede que a peça seja arrastada novamente
            Debug.Log($"✅ Peça {gameObject.name} posicionada corretamente!");
            QuebraCabecaController.Instance.VerificarVitoria();
        }
        else
        {
            Debug.Log($"❌ Peça {gameObject.name} NÃO está no local certo! Retornando à posição inicial.");
            rectTransform.anchoredPosition = posicaoInicial; // Volta para a posição inicial
            AudioManager.Instance.TocarSomErro();

        }
    }
}
