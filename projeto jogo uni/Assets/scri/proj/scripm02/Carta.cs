using UnityEngine;

public class Carta : MonoBehaviour
{
    public Sprite ImagemOculta; // A imagem de fundo (oculta)
    public Sprite ImagemRevelada; // A imagem real da carta
    private SpriteRenderer spriteRenderer;
    public int index; // ID �nico da carta

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ImagemOculta; // Come�a oculta
    }

    public void DefinirImagem(Sprite imagem)
    {
        ImagemRevelada = imagem;
    }

    public void Revelar()
    {
        spriteRenderer.sprite = ImagemRevelada; // Muda para a imagem real
    }

    public void Ocultar()
    {
        spriteRenderer.sprite = ImagemOculta; // Volta para a oculta
    }

    public void Desabilitar()
    {
        gameObject.SetActive(false); // Remove do jogo quando encontrada
    }
}
