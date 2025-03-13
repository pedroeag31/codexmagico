using UnityEngine;

public class ArrastarPeca : MonoBehaviour
{
    private bool arrastando = false;
    private Vector3 offset;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position; // Guarda a posição original da peça
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector3 toquePosicao = Camera.main.ScreenToWorldPoint(toque.position);
            toquePosicao.z = 0; // Garante que a peça fique no mesmo plano 2D

            switch (toque.phase)
            {
                case TouchPhase.Began:
                    Collider2D hit = Physics2D.OverlapPoint(toquePosicao);
                    if (hit != null)
                    {
                        Debug.Log("Toque detectado em: " + hit.gameObject.name); // Debug: detectando toque
                        if (hit.gameObject == gameObject)
                        {
                            arrastando = true;
                            offset = transform.position - toquePosicao;
                            Debug.Log("Arrastando: " + gameObject.name); // Debug: iniciou o arrasto
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (arrastando)
                    {
                        transform.position = toquePosicao + offset; // Move a peça
                    }
                    break;

                case TouchPhase.Ended:
                    if (arrastando)
                    {
                        arrastando = false;
                        Debug.Log("Soltou: " + gameObject.name); // Debug: soltou a peça

                        // Verifica se a peça está no lugar certo (ajuste a distância conforme necessário)
                        if (Vector3.Distance(transform.position, posicaoInicial) < 0.5f)
                        {
                            transform.position = posicaoInicial; // Encaixa no slot
                            Debug.Log("Peça encaixada corretamente!");
                        }
                        else
                        {
                            Debug.Log("Peça solta no lugar errado!");
                        }
                    }
                    break;
            }
        }
    }
}
