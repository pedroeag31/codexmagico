using System.Collections.Generic;
using UnityEngine;

public class SequenciaDePontos : MonoBehaviour
{
    [Tooltip("Arraste os pontos na ordem desejada")]
    public List<Transform> pontos = new List<Transform>();

    public Transform GetPonto(int index)
    {
        if (index >= 0 && index < pontos.Count)
            return pontos[index];
        return null;
    }

    public int TotalPontos()
    {
        return pontos.Count;
    }
}
