using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource somVirarPagina;
    public AudioSource somErro;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TocarSomVirarPagina()
    {
        somVirarPagina.Play();
    }

    public void TocarSomErro()
    {
        somErro.Play();
    }
}
