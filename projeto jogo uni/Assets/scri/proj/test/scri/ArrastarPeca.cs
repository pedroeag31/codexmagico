using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarPeca : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 posicaoInicial;
    private Transform slotCorreto;

    public void Start()
    {
        posicaoInicial = transform.position;
    }

    public void DefinirSlotCorreto(Transform slot)
    {
        slotCorreto = slot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Opcional: Aumentar a escala da peça ao arrastar
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector3.Distance(transform.position, slotCorreto.position) < 50f)
        {
            transform.position = slotCorreto.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false; // Impede que a peça seja arrastada novamente
            QuebraCabecaController.Instance.VerificarVitoria();
        }
        else
        {
            transform.position = posicaoInicial;
        }

        transform.localScale = Vector3.one; // Retorna ao tamanho normal
    }
}
