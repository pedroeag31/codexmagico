using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarPeca : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 posicaoInicial;
    private CanvasGroup canvasGroup;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        posicaoInicial = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.7f; // Deixa a peça semi-transparente ao arrastar
        canvasGroup.blocksRaycasts = false; // Permite que o Raycast passe pela peça
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition; // Move a peça com o cursor
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Verifica se está na posição correta
        if (Vector3.Distance(rectTransform.position, posicaoInicial) < 50f)
        {
            rectTransform.position = posicaoInicial; // Encaixa a peça
            Debug.Log("Pedaço encaixado!");
        }
        else
        {
            Debug.Log("Posição errada!");
        }
    }
}
