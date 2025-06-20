using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton pattern para acceso f�cil
    public static GameManager Instance { get; private set; }
    public static Action<GamePhase> OnPhaseChanged { get; internal set; }

    // Enum para definir las fases del juego
    public enum GamePhase
    {
        Phase1,
        Phase2,
        Phase3,
        
    }

    [SerializeField] private GamePhase currentPhase = GamePhase.Phase1;

    private void Awake()
    {
        // Implementaci�n del singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GamePhase GetCurrentPhase()
    {
        return currentPhase;
    }

    public void ChangePhase(GamePhase newPhase)
    {
        // Salir de la fase actual
        ExitCurrentPhase();

        // Cambiar a la nueva fase
        currentPhase = newPhase;

        // Entrar en la nueva fase
        EnterNewPhase(newPhase);
    }

    private void ExitCurrentPhase()
    {
        switch (currentPhase)
        {
            case GamePhase.Phase1:
                // L�gica para salir del men� principal
                break;
            case GamePhase.Phase2:
                // L�gica para salir de la exploraci�n
                break;
            case GamePhase.Phase3:
                break;
        }
    }

    private void EnterNewPhase(GamePhase newPhase)
    {
        /*switch (newPhase)
        {
            case GamePhase.Phase1:
                // L�gica para entrar al men� principal
                break;
            case GamePhase.Phase2:
                // L�gica para la exploraci�n
                SetupExplorationEnvironment();
                break;
            case GamePhase.Combat:
                // L�gica para el combate
                SetupCombatEnvironment();
                break;
                // ... otros casos
        }*/

        Debug.Log($"Entrando en fase: {newPhase}");
    }
}
