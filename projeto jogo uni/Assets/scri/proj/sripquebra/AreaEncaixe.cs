using UnityEngine;
using UnityEngine.EventSystems;

public class AreaEncaixe : MonoBehaviour, IDropHandler
{
    public GameObject pecaCorreta; // Referência ao pedaço correto

    public void OnDrop(PointerEventData eventData)
    {
        GameObject pecaArrastada = eventData.pointerDrag;

        if (pecaArrastada != null && pecaArrastada == pecaCorreta)
        {
            pecaArrastada.transform.position = transform.position; // Encaixa no lugar certo
            pecaArrastada.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Debug.Log("Peça encaixada corretamente!");
        }
        else
        {
            Debug.Log("Essa peça não pertence aqui!");
        }
    }
}
