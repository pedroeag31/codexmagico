using UnityEngine;

public class ArrastarPeca : MonoBehaviour
{
    private bool arrastando = false;
    private Vector3 offset;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position; // Guarda a posi��o original da pe�a
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector3 toquePosicao = Camera.main.ScreenToWorldPoint(toque.position);
            toquePosicao.z = 0; // Garante que a pe�a fique no mesmo plano 2D

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
                        transform.position = toquePosicao + offset; // Move a pe�a
                    }
                    break;

                case TouchPhase.Ended:
                    if (arrastando)
                    {
                        arrastando = false;
                        Debug.Log("Soltou: " + gameObject.name); // Debug: soltou a pe�a

                        // Verifica se a pe�a est� no lugar certo (ajuste a dist�ncia conforme necess�rio)
                        if (Vector3.Distance(transform.position, posicaoInicial) < 0.5f)
                        {
                            transform.position = posicaoInicial; // Encaixa no slot
                            Debug.Log("Pe�a encaixada corretamente!");
                        }
                        else
                        {
                            Debug.Log("Pe�a solta no lugar errado!");
                        }
                    }
                    break;
            }
        }
    }
}
