using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necessário para manipular a UI

public class MapNavigationManager : MonoBehaviour
{
    // Nomes das cenas que representam os locais
    public string[] sceneNames;

    // Imagens de fundo correspondentes a cada local
    public Sprite[] backgroundImages;

    // Referência para o componente Image do fundo
    public Image backgroundImage;

    private int currentLocationIndex;

    // Referências aos botões de seta e entrar
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject enterButton;

    private void Start()
    {
        // Inicia no último local (mais à direita)
        currentLocationIndex = sceneNames.Length - 1;
        UpdateUI();
    }

    // Atualiza a UI com base no local atual
    private void UpdateUI()
    {
        // Atualiza as setas
        leftArrow.SetActive(currentLocationIndex > 0);
        rightArrow.SetActive(currentLocationIndex < sceneNames.Length - 1);
        enterButton.SetActive(true); // Sempre ativo, mas pode ser ajustado

        // Atualiza a imagem de fundo
        if (backgroundImage != null && currentLocationIndex >= 0 && currentLocationIndex < backgroundImages.Length)
        {
            backgroundImage.sprite = backgroundImages[currentLocationIndex];
        }
    }

    // Navega para o local à esquerda
    public void MoveLeft()
    {
        if (currentLocationIndex > 0)
        {
            currentLocationIndex--;
            UpdateUI();
        }
    }

    // Navega para o local à direita
    public void MoveRight()
    {
        if (currentLocationIndex < sceneNames.Length - 1)
        {
            currentLocationIndex++;
            UpdateUI();
        }
    }

    // Entra no local selecionado
    public void EnterLocation()
    {
        if (currentLocationIndex >= 0 && currentLocationIndex < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[currentLocationIndex]);
        }
    }
}