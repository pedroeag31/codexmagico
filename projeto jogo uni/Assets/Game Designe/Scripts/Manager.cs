using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public DialogManager dialogManager; // Referência ao DialogManager
    public GameObject miniGamePanel; // Painel que contém todos os elementos do mini game

    void Start()
    {
        // Esconde o mini game no início
        miniGamePanel.SetActive(false);

        // Verifica se o diálogo terminou
        StartCoroutine(WaitForDialogToFinish());
    }

    IEnumerator WaitForDialogToFinish()
    {
        // Espera o diálogo terminar
        while (!dialogManager.IsDialogFinished())
        {
            yield return null;
        }

        // Mostra o mini game após o fim do diálogo
        miniGamePanel.SetActive(true);
    }
}