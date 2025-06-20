using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton pattern para acceso fácil
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
        // Implementación del singleton
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
                // Lógica para salir del menú principal
                break;
            case GamePhase.Phase2:
                // Lógica para salir de la exploración
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
                // Lógica para entrar al menú principal
                break;
            case GamePhase.Phase2:
                // Lógica para la exploración
                SetupExplorationEnvironment();
                break;
            case GamePhase.Combat:
                // Lógica para el combate
                SetupCombatEnvironment();
                break;
                // ... otros casos
        }*/

        Debug.Log($"Entrando en fase: {newPhase}");
    }
}
