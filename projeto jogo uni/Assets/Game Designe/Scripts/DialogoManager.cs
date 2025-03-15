using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogPart
    {
        [TextArea(3, 10)] // Facilita a edição de textos longos no Inspector
        public string text; // Texto da fala
        public float typingSpeed = 0.05f; // Velocidade da digitação
    }

    public DialogPart[] dialogParts; // Partes do diálogo
    public Text dialogText; // Componente de texto da UI
    public GameObject dialogPanel; // Painel de diálogo

    private int currentPartIndex = 0;
    private bool isTyping = false;
    private bool dialogFinished = false;

    void Start()
    {
        StartDialog();
    }

    void StartDialog()
    {
        dialogPanel.SetActive(true); // Ativa o painel de diálogo
        currentPartIndex = 0;
        dialogFinished = false;
        StartCoroutine(TypeText(dialogParts[currentPartIndex].text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = ""; // Limpa o texto

        // Efeito de digitação
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(dialogParts[currentPartIndex].typingSpeed);
        }

        isTyping = false;
    }

    void Update()
    {
        // Avança o diálogo ao clicar na tela
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Completa o texto imediatamente
                StopAllCoroutines();
                dialogText.text = dialogParts[currentPartIndex].text;
                isTyping = false;
            }
            else
            {
                NextPart();
            }
        }
    }

    void NextPart()
    {
        currentPartIndex++;

        if (currentPartIndex < dialogParts.Length)
        {
            StartCoroutine(TypeText(dialogParts[currentPartIndex].text));
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        dialogFinished = true;
        dialogPanel.SetActive(false); // Desativa o painel de diálogo
    }

    public bool IsDialogFinished()
    {
        return dialogFinished;
    }
}