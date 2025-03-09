using UnityEngine;
using UnityEngine.EventSystems;

public class AreaEncaixe : MonoBehaviour, IDropHandler
{
    public GameObject pecaCorreta; // Refer�ncia ao peda�o correto

    public void OnDrop(PointerEventData eventData)
    {
        GameObject pecaArrastada = eventData.pointerDrag;

        if (pecaArrastada != null && pecaArrastada == pecaCorreta)
        {
            pecaArrastada.transform.position = transform.position; // Encaixa no lugar certo
            pecaArrastada.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Debug.Log("Pe�a encaixada corretamente!");
        }
        else
        {
            Debug.Log("Essa pe�a n�o pertence aqui!");
        }
    }
}
