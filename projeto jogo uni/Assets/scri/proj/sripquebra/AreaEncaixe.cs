using UnityEngine;

public class AreaEncaixe : MonoBehaviour
{
    public GameObject pecaCorreta; // Peça que deve encaixar aqui
    private bool encaixado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!encaixado && other.gameObject == pecaCorreta)
        {
            other.transform.position = transform.position; // Ajusta a posição
            other.GetComponent<ArrastarPeca>().BloquearMovimento(); // Bloqueia o movimento
            encaixado = true;
            Debug.Log("✅ Peça encaixada corretamente!");
        }
        else
        {
            Debug.Log("❌ Peça errada!");
        }
    }
}
