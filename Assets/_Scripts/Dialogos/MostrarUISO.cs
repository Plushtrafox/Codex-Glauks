using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MostrarUISO", menuName = "ScriptableObjects/MostrarUISO", order = 1)]
public class MostrarUISO : ScriptableObject
{

    public Action EventoMostrarUI;
    public Action EventoOcultarUI;

}
