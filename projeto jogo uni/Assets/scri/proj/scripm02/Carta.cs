using UnityEngine;

public class Carta : MonoBehaviour
{
    public Sprite ImagemOculta; // Imagem oculta no início
    public Sprite ImagemRevelada; // Imagem revelada quando o jogador interage
    public int index; // Índice da carta

    private JogoDaMemoria jogo;

    void Start()
    {
        jogo = FindObjectOfType<JogoDaMemoria>();
        GetComponent<SpriteRenderer>().sprite = ImagemOculta;
    }

    // Definir a imagem que será revelada
    public void DefinirImagem(Sprite imagem)
    {
        ImagemRevelada = imagem;
    }

    // Função para ser chamada quando a carta for clicada
    void OnMouseDown()
    {
        jogo.RevelarCarta(gameObject);
    }

    // Função para desabilitar a carta quando um par correto for feito
    public void Desabilitar()
    {
        gameObject.SetActive(false);
    }
}
