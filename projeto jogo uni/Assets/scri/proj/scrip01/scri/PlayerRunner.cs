using UnityEngine;

public class PlayerRunner : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 10f;
    public GameObject ataquePrefab;
    public Transform pontoDeAtaque;
    public LayerMask inimigoLayer;
    public Transform detectorChao;
    public LayerMask chaoLayer;

    private Rigidbody2D rb;
    private bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimento automático para frente
        rb.linearVelocity = new Vector2(velocidade, rb.linearVelocity.y);

        // Detecta toque na tela
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2 && noChao)
                {
                    // TOQUE ESQUERDO → PULAR
                    Pular();
                }
                else if (touch.position.x >= Screen.width / 2)
                {
                    // TOQUE DIREITO → ATACAR
                    Atacar();
                }
            }
        }

        // Verifica se está no chão
        noChao = Physics2D.OverlapCircle(detectorChao.position, 0.1f, chaoLayer);
    }

    void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo);
        noChao = false; // Evita pular várias vezes seguidas
    }

    void Atacar()
    {
        GameObject novoDisparo = Instantiate(ataquePrefab, pontoDeAtaque.position, Quaternion.identity);
        novoDisparo.GetComponent<Disparo>().DefinirDirecao(Vector2.right); // Define a direção do disparo
    }
}

