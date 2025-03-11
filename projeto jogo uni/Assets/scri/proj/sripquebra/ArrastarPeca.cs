using UnityEngine;

public class ArrastarPeca : MonoBehaviour
{
    private Vector3 posicaoInicial;
    private bool arrastando = false;
    private Vector3 offset; // Ajuste para tocar corretamente na pe�a

    void Start()
    {
        posicaoInicial = transform.position; // Salva a posi��o inicial
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 toquePosicao = Camera.main.ScreenToWorldPoint(touch.position);
            toquePosicao.z = 0; // Mant�m no mesmo plano 2D

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Verifica se o toque atingiu a pe�a
                    Collider2D hit = Physics2D.OverlapPoint(toquePosicao);
                    if (hit != null && hit.gameObject == gameObject)
                    {
                        arrastando = true;
                        offset = transform.position - toquePosicao; // Ajuste para pegar no ponto certo
                    }
                    break;

                case TouchPhase.Moved:
                    if (arrastando)
                    {
                        transform.position = toquePosicao + offset;
                    }
                    break;

                case TouchPhase.Ended:
                    arrastando = false;

                    // Verifica se a pe�a est� pr�xima do local correto
                    if (Vector3.Distance(transform.position, posicaoInicial) < 0.5f)
                    {
                        transform.position = posicaoInicial;
                        Debug.Log("Peda�o encaixado!");
                    }
                    else
                    {
                        Debug.Log("Posi��o errada!");
                    }
                    break;
            }
        }
    }
}
