using UnityEngine;

public class Disparo : MonoBehaviour
{
    public float velocidade = 10f;
    public float tempoDeVida = 2f;
    private Vector2 direcao;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    public void DefinirDirecao(Vector2 novaDirecao)
    {
        direcao = novaDirecao;
    }
}
