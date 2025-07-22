using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogoManager", menuName = "ScriptableObjects/DialogoManagerSO", order = 1)]
public class DialogoManager : ScriptableObject
{
    public Action MostrarHUD;
    public Action OcultarHUD;

}
