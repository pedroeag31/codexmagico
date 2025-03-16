using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Adicionado para gerenciamento de cena

public class GameManagerDesigner : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int correctAnswerIndex;
    }

    // Referências da UI
    public Text questionText;
    public Text scoreText;
    public Button[] answerButtons;
    public Button voltarButton; // Botão novo para voltar à navegação

    // Efeitos sonoros
    public AudioClip correctSound;
    public AudioClip wrongSound;
    private AudioSource audioSource;

    // Lista de perguntas e variáveis do jogo
    public List<Question> questions = new List<Question>();
    private int currentQuestionIndex = 0;
    private int score = 0;

    void Start()
    {
        // Inicializa o AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        // Configura botão de voltar como inativo inicialmente
        voltarButton.gameObject.SetActive(false);

        // Inicializa perguntas e carrega a primeira
        LoadQuestion(currentQuestionIndex);
        UpdateScoreDisplay();
    }

    void LoadQuestion(int index)
    {
        if (index < 0 || index >= questions.Count) return;

        // Atualiza o texto da pergunta
        questionText.text = questions[index].question;

        // Configura os botões com as respostas
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Define o texto do botão
            answerButtons[i].GetComponentInChildren<Text>().text = questions[index].answers[i];

            // Remove listeners antigos e adiciona um novo
            int buttonIndex = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(buttonIndex));

            // Reseta a cor do botão para a cor original
            answerButtons[i].image.color = Color.white;
        }
    }

    public void CheckAnswer(int selectedIndex)
    {
        // Verifica se a resposta está correta
        bool isCorrect = selectedIndex == questions[currentQuestionIndex].correctAnswerIndex;

        // Feedback visual
        if (isCorrect)
        {
            answerButtons[selectedIndex].image.color = Color.green;
            PlaySound(correctSound);
            score += 10;
        }
        else
        {
            answerButtons[selectedIndex].image.color = Color.red;
            PlaySound(wrongSound);
        }

        UpdateScoreDisplay();
        Invoke("LoadNextQuestion", 1f);
    }

    void LoadNextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Count)
        {
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            // Fim do jogo: mostra texto, esconde botões e ativa botão de voltar
            questionText.text = "Fim do Jogo! Pontuação: " + score;
            foreach (Button btn in answerButtons)
            {
                btn.gameObject.SetActive(false);
            }
            voltarButton.gameObject.SetActive(true); // Ativa o botão de voltar
        }
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Pontos: " + score;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Método novo para carregar a cena de navegação
    public void VoltarParaNavegacao()
    {
        SceneManager.LoadScene("Navegação");
    }
}