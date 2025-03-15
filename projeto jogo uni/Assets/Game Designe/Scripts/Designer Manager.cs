using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
            answerButtons[selectedIndex].image.color = Color.green; // Botão fica verde
            PlaySound(correctSound); // Toca som de acerto
            score += 10; // Adiciona pontos
        }
        else
        {
            answerButtons[selectedIndex].image.color = Color.red; // Botão fica vermelho
            PlaySound(wrongSound); // Toca som de erro
        }

        // Atualiza a pontuação
        UpdateScoreDisplay();

        // Avança para a próxima pergunta após um pequeno delay
        Invoke("LoadNextQuestion", 1f); // 1 segundo de delay
    }

    void LoadNextQuestion()
    {
        currentQuestionIndex++;

        // Verifica se ainda há perguntas
        if (currentQuestionIndex < questions.Count)
        {
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            // Fim do jogo
            questionText.text = "Fim do Jogo! Pontuação: " + score;
            foreach (Button btn in answerButtons)
            {
                btn.gameObject.SetActive(false); // Esconde os botões
            }
        }
    }

    void UpdateScoreDisplay()
    {
        // Atualiza o texto da pontuação
        scoreText.text = "Pontos: " + score;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // Toca o som
        }
    }
}