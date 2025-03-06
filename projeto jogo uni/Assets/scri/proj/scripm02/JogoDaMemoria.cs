using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para atualizar a UI de tempo

public class JogoDaMemoria : MonoBehaviour
{
    public GameObject cartaPrefab; // Prefab da carta (a carta com a imagem oculta)
    public Sprite[] imagens; // Sprites das imagens mágicas a serem reveladas
    public int numPares = 6; // Número de pares no jogo
    public float tempoLimite = 60f; // Tempo limite para o jogo

    private List<GameObject> cartas; // Lista de cartas no jogo
    private GameObject cartaSelecionada1; // Primeira carta selecionada
    private GameObject cartaSelecionada2; // Segunda carta selecionada
    private bool comparando = false; // Flag para evitar múltiplas seleções
    private float tempoRestante; // Tempo restante no contador

    public Text tempoText; // UI para mostrar o tempo restante

    void Start()
    {
        cartas = new List<GameObject>();
        tempoRestante = tempoLimite; // Inicializa o tempo restante
        GerarCartas();
    }

    void Update()
    {
        // Atualiza o contador de tempo
        tempoRestante -= Time.deltaTime;
        tempoText.text = "Tempo: " + Mathf.Max(0, Mathf.FloorToInt(tempoRestante)).ToString(); // Exibe o tempo restante

        // Verifica se o tempo acabou
        if (tempoRestante <= 0)
        {
            TempoAcabou();
        }
    }

    // Função para gerar as cartas no tabuleiro
    void GerarCartas()
    {
        List<Sprite> spritesParaSelecionar = new List<Sprite>();

        // Adiciona os pares de imagens para o jogo
        for (int i = 0; i < numPares; i++)
        {
            spritesParaSelecionar.Add(imagens[i]);
            spritesParaSelecionar.Add(imagens[i]); // Adiciona o par
        }

        // Embaralha as imagens
        for (int i = 0; i < spritesParaSelecionar.Count; i++)
        {
            Sprite temp = spritesParaSelecionar[i];
            int randomIndex = Random.Range(i, spritesParaSelecionar.Count);
            spritesParaSelecionar[i] = spritesParaSelecionar[randomIndex];
            spritesParaSelecionar[randomIndex] = temp;
        }

        // Instancia as cartas no grid
        for (int i = 0; i < spritesParaSelecionar.Count; i++)
        {
            GameObject novaCarta = Instantiate(cartaPrefab, transform);
            novaCarta.GetComponent<Carta>().DefinirImagem(spritesParaSelecionar[i]); // Define a imagem revelada
            novaCarta.GetComponent<Carta>().index = i;
            cartas.Add(novaCarta);
        }
    }

    // Função chamada quando uma carta é clicada
    public void RevelarCarta(GameObject carta)
    {
        if (comparando || carta == cartaSelecionada1) return; // Impede múltiplas seleções

        carta.GetComponent<SpriteRenderer>().sprite = carta.GetComponent<Carta>().ImagemRevelada;

        if (cartaSelecionada1 == null)
        {
            cartaSelecionada1 = carta;
        }
        else
        {
            cartaSelecionada2 = carta;
            comparando = true;
            StartCoroutine(CompararCartas());
        }
    }

    // Função para comparar as duas cartas
    IEnumerator CompararCartas()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo para ver as cartas

        if (cartaSelecionada1.GetComponent<SpriteRenderer>().sprite == cartaSelecionada2.GetComponent<SpriteRenderer>().sprite)
        {
            // Se as cartas forem iguais
            cartaSelecionada1.GetComponent<Carta>().Desabilitar();
            cartaSelecionada2.GetComponent<Carta>().Desabilitar();
        }
        else
        {
            // Se as cartas não forem iguais, oculta novamente
            cartaSelecionada1.GetComponent<SpriteRenderer>().sprite = cartaSelecionada1.GetComponent<Carta>().ImagemOculta;
            cartaSelecionada2.GetComponent<SpriteRenderer>().sprite = cartaSelecionada2.GetComponent<Carta>().ImagemOculta;
        }

        // Reseta as seleções
        cartaSelecionada1 = null;
        cartaSelecionada2 = null;
        comparando = false;
    }

    // Função chamada quando o tempo acaba
    void TempoAcabou()
    {
        Debug.Log("Tempo esgotado! O jogo acabou.");
        // Aqui você pode adicionar a lógica para finalizar o jogo, como mostrar uma tela de falha ou reiniciar
        // Por exemplo:
        // SceneManager.LoadScene("GameOver");
    }
}
