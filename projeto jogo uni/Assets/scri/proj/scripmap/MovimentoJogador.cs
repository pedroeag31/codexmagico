using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade de movimento
    private Vector2 destino; // Ponto de destino onde o jogador vai se mover
    private bool movendo = false; // Flag para saber se o jogador est� se movendo
    private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obt�m o Rigidbody2D do jogador
    }

    void Update()
    {
        // Verifica se h� um toque na tela
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            Vector2 posicaoToque = Camera.main.ScreenToWorldPoint(toque.position); // Converte para coordenadas do mundo

            if (toque.phase == TouchPhase.Began) // Quando o toque come�a
            {
                destino = posicaoToque; // Define o destino para onde o jogador deve se mover
                movendo = true; // O jogador come�ar� a se mover
            }
        }
    }

    void FixedUpdate()
    {
        if (movendo)
        {
            // Calcula a dire��o para o destino
            Vector2 direcao = (destino - rb.position).normalized;

            // Move o jogador em dire��o ao destino
            rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);

            // Verifica se o jogador chegou ao destino
            if (Vector2.Distance(rb.position, destino) < 0.2f) // Ajuste o valor conforme necess�rio
            {
                movendo = false; // O movimento � conclu�do quando o jogador chega perto do destino
            }
        }
    }

    // Fun��o p�blica para acessar o destino
    public Vector2 Destino
    {
        get { return destino; }
    }
}
