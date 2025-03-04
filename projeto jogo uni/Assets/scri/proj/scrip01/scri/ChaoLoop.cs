using UnityEngine;

public class ChaoLoop : MonoBehaviour
{
    public float velocidade = 5f;
    public float larguraChao; // Define a largura do ch�o (exemplo: 10)

    private Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        // Move o ch�o para a esquerda
        transform.position += Vector3.left * velocidade * Time.deltaTime;

        // Se o ch�o sair da tela, reposiciona ele para frente
        if (transform.position.x <= posInicial.x - larguraChao)
        {
            ReiniciarPosicao();
        }
    }

    void ReiniciarPosicao()
    {
        transform.position += Vector3.right * (larguraChao * 2);
    }
}
