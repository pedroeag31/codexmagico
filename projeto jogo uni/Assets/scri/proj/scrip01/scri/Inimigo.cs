using UnityEngine;
using UnityEngine.UI;

public class Inimigo : MonoBehaviour
{
    public float vida = 30f;
    public Image barraDeVida; // Arraste a imagem "Vida" aqui

    public void ReceberDano(float dano)
    {
        vida -= dano;
        AtualizarBarraDeVida();
        Debug.Log(gameObject.name + " recebeu " + dano + " de dano! Vida restante: " + vida);

        if (vida <= 0)
        {
            Morrer();
        }
    }

    void AtualizarBarraDeVida()
    {
        barraDeVida.fillAmount = vida / 30f; // 30 é a vida máxima
    }

    void Morrer()
    {
        Debug.Log(gameObject.name + " morreu!");
        Destroy(gameObject);
    }
}
